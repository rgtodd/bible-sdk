from pandas import DataFrame, read_csv
from pprint import pprint

import biblesdk.columns as bc
from biblesdk.report import Report


class Analyzer:
    """Produces reports for the New Testament listing the most commonly used words. The word_count property
    specifies the number words in the report.

    There are two types of reports:

    1. A report of the most frequently used words across the entire New Testament.

    2. Reports for every book in the New Testatment.  The report lists the most frequently used words in the book, plus any additional words 
    that also appear in the New Testament report regardless of usage.  Words unique to the book (i.e., that do not appear in the New Testament report)
    are highlighted.
    """

    def __init__(self, word_count: int):
        """Initializes a new Analyzer object.

        Args:
            word_count (int): The number of words to be included in each report.
        """
        self.enable_dump: bool = False
        self.word_count: int = word_count

    def load_data(self, morphgnt_csv: str, lexemes_csv: str, mounce_txt: str):
        """Loads data into the analyzer.

        Args:
            morphgnt_csv (str): The name and location of the morphgnt.csv file.
            lexemes_csv (str): The name and location of the lexemes.csv file.
            mounce_txt (str): The name and location of the mounce.txt file.
        """

        self.DF_MORPHGNT: DataFrame = read_csv(morphgnt_csv, index_col=bc.INDEX)
        self.DF_LEXEMES: DataFrame = read_csv(lexemes_csv, index_col=bc.LEMMA)
        self.DF_MOUNCE: DataFrame = (
            read_csv(
                mounce_txt,
                sep="\t",
                names=[bc.GK, bc.MOUNCE_CHAPTER],
                index_col=bc.GK,
                dtype={bc.GK: "object", bc.MOUNCE_CHAPTER: "object"},
            )
            .groupby(bc.GK)[bc.MOUNCE_CHAPTER]
            .apply(list)
            .to_frame(bc.MOUNCE_CHAPTER)
        )
        self._dump(self.DF_MORPHGNT, "DF_MORPHGNT")
        self._dump(self.DF_LEXEMES, "DF_LEXEMES")

        self.TOTAL_WORD_COUNT: int = len(self.DF_MORPHGNT)
        self.TOTAL_LEXEME_COUNT: int = len(self.DF_LEXEMES)

    def get_new_testament_report(self) -> Report:
        """Produces the New Testament word report.

        Returns:
            Report: The Report object.
        """

        report_df = self._create_report_df(self.DF_MORPHGNT).head(self.word_count)

        percentage = report_df[bc.WORD_PERCENTAGE_CUMULATIVE].max()

        df = report_df.drop(columns=[bc.WORD_PERCENTAGE, bc.WORD_PERCENTAGE_CUMULATIVE])

        return Report(
            df,
            {
                "Total Word Count": len(self.DF_MORPHGNT),
                "Unique Word Count": len(report_df),
                "Vocabulary Word Count": len(report_df),
                "Vocabulary Percentage": f"{percentage:.2%}",
            },
            self.word_count,
        )

    def get_book_report(
        self, book: int, chapter: int = None, add_nt_word_index: bool = False
    ) -> Report:
        """Produces a word report for the specified New Testament book.

        Args:
            book (int): Index of the book.
            chapter (int, optional): Chapter number. Defaults to None.
            add_nt_word_index (bool, optional): If True, the index of the word in the New Testament report is included in the report. Defaults to False.

        Returns:
            Report: The Report object.
        """

        df_morphgnt_book = self.DF_MORPHGNT[(self.DF_MORPHGNT[bc.BOOK] == book)]
        if chapter:
            df_morphgnt_book = df_morphgnt_book[
                (df_morphgnt_book[bc.CHAPTER] == chapter)
            ]
        self._dump(df_morphgnt_book, "df_morphgnt_book")

        report_df = self._create_report_df(df_morphgnt_book)

        if add_nt_word_index:
            new_testament_report_df = self._create_report_df(self.DF_MORPHGNT)
            new_testament_word_index = new_testament_report_df[bc.WORD_INDEX]
            report_df.insert(
                loc=1,
                column=bc.NEW_TESTAMENT_WORD_INDEX,
                value=new_testament_word_index,
            )

        book_top_words = report_df[
            (report_df[bc.WORD_INDEX] <= self.word_count)
            | (report_df[bc.NEW_TESTAMENT_WORD_INDEX] <= self.word_count)
        ]
        book_top_words = book_top_words.drop(columns=[bc.WORD_PERCENTAGE_CUMULATIVE])
        book_top_words[bc.WORD_PERCENTAGE_CUMULATIVE] = book_top_words[
            bc.WORD_PERCENTAGE
        ].cumsum()

        new_words = book_top_words[
            (book_top_words[bc.WORD_INDEX] <= self.word_count)
            & (book_top_words[bc.NEW_TESTAMENT_WORD_INDEX] > self.word_count)
        ]

        df = book_top_words.drop(
            columns=[bc.WORD_PERCENTAGE, bc.WORD_PERCENTAGE_CUMULATIVE]
        )

        return Report(
            df,
            {
                "Total Word Count": len(df_morphgnt_book),
                "Unique Word Count": len(report_df),
                "New Vocabulary Word Count": len(new_words),
                "Total Vocabulary Percentage": f"{book_top_words[bc.WORD_PERCENTAGE_CUMULATIVE].max():.2%}",
            },
            self.word_count,
        )

    def _create_report_df(self, df_morphgnt: DataFrame) -> DataFrame:

        total_word_count = len(df_morphgnt)

        s_lemma_word_counts = df_morphgnt.groupby(bc.LEMMA).size()
        self._dump(s_lemma_word_counts, "S_LEMMA_WORD_COUNTS")

        df_analysis = s_lemma_word_counts.to_frame(name=bc.WORD_COUNT)
        df_analysis.index.name = bc.LEMMA
        df_analysis[bc.WORD_PERCENTAGE] = df_analysis[bc.WORD_COUNT] / total_word_count
        self._dump(df_analysis, "DF_ANALYSIS")

        df_analysis_sorted = df_analysis.sort_values(
            bc.WORD_PERCENTAGE, ascending=False
        )
        df_analysis_sorted[bc.WORD_INDEX] = range(1, len(df_analysis_sorted) + 1)
        df_analysis_sorted[bc.WORD_PERCENTAGE_CUMULATIVE] = df_analysis_sorted[
            bc.WORD_PERCENTAGE
        ].cumsum()
        self._dump(df_analysis_sorted, "DF_ANALYSIS_SORTED")

        df_merged = df_analysis_sorted.join(self.DF_LEXEMES).join(
            self.DF_MOUNCE, on=bc.GK
        )
        self._dump(df_merged, "DF_MERGED")

        df_merged[bc.LEXICAL_ENTRY] = df_merged[bc.DODSON_ENTRY].combine_first(
            df_merged[bc.BDAG_ENTRY]
        )

        df_report = df_merged.reindex(
            columns=[
                bc.WORD_INDEX,
                bc.PART_OF_SPEECH,
                bc.LEXICAL_ENTRY,
                bc.GLOSS,
                bc.STRONGS,
                bc.GK,
                bc.MOUNCE_CHAPTER,
                bc.WORD_COUNT,
                bc.WORD_PERCENTAGE,
                bc.WORD_PERCENTAGE_CUMULATIVE,
            ]
        )
        self._dump(df_report, "DF_REPORT")

        return df_report

    def _dump(self, object: any, name: str):

        if self.enable_dump:
            print(f"===== {name}")
            print(object.__class__.__name__)
            print("-----")
            pprint(vars(object))
            print("-----")
            pprint(object)
