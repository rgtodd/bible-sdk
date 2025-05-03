import pandas as pd
import biblesdk.constants as bc
from pprint import pprint


class Report:
    """
    Defines a report for the specified DataFrame.
    """

    def __init__(self, df, properties, word_index_highlight_threshold):
        """
        Creates a new

        Keyword arguments:
        df -- the DataFrame associated with this report.
        word_index_highlight_threshold -- word index beyond which words are highlighted in the report. 
                                          Used to highlight words that are not part of standard word lists.
        """

        self.df = df
        self.properties = dict(properties)
        self.word_index_highlight_threshold = word_index_highlight_threshold

    def get_styler(self, highlight_nt_rank=True):
        """
        Create a styler for the DataFrame associated with this report.
        """

        df_report = self.df

        report_styler = (
            df_report.style.hide(axis="index")
            .format(
                # {WORD_PERCENTAGE: "{:.2%}", WORD_PERCENTAGE_CUMULATIVE: "{:.2%}"},
                {
                    bc.STRONGS: _format_lexical_number,
                    bc.GK: _format_lexical_number,
                    bc.MOUNCE_CHAPTER: _format_mounce,
                    bc.WORD_COUNT: "{:,}",
                },
                precision=2,
                na_rep="",
            )
            .set_properties(
                subset=[
                    bc.GLOSS,
                    bc.LEXICAL_ENTRY,
                    bc.PART_OF_SPEECH,
                ],
                **{"text-align": "left"},
            )
            .set_properties(
                subset=[
                    bc.WORD_COUNT,
                    bc.STRONGS,
                    bc.GK,
                    bc.MOUNCE_CHAPTER,
                ],  # , WORD_PERCENTAGE, WORD_PERCENTAGE_CUMULATIVE],
                **{"text-align": "right"},
            )
            .set_table_styles([{"selector": "th", "props": [("text-align", "left")]}])
            # .bar(subset=[WORD_PERCENTAGE_CUMULATIVE], color="LightBlue", vmax=1)
        )

        if highlight_nt_rank & (bc.NEW_TESTAMENT_WORD_INDEX in df_report):
            report_styler = report_styler.apply(
                lambda df: _select_col(df, self.word_index_highlight_threshold),
                axis=None,
            )

        return report_styler


def _format_lexical_number(value):
    try:
        int_value = int(value)
        return f"{int_value:04d}"
    except ValueError:
        return value


def _format_mounce(value):
    return ",".join(value)


def _select_col(df, word_index_highlight_threshold):
    c1 = "background-color: LightGreen"
    c2 = ""
    mask = df[bc.NEW_TESTAMENT_WORD_INDEX] > word_index_highlight_threshold
    df1 = pd.DataFrame(c2, index=df.index, columns=df.columns)
    df1.loc[mask, bc.WORD_INDEX] = c1
    return df1