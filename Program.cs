using System;
using System.Text;
using System.Collections;

namespace idwall
{
    class Program
    {
        static int LINE_MAX_LENGTH = 40;
        static int LINE_DEF_LENGTH = 20;
        static bool JUSTIFY_LAST_PARAGRAPH_LINE = true;

        /// <summary>
        /// This console application is responsible for performing the basic tests for the text formatter challenge.
        /// </summary>
        /// <param name="args">no need of arguments.</param>
        static void Main(string[] args)
        {
            Console.WriteLine(">>>Test #0 =================================================");
            Console.WriteLine(JustifyText(30, "A Turkish man who lives in Istanbul, Hakan doesn't work for any retail firm. Instead he is a stage lighting manager in a theatre.\nYet he is continuing to help retail companies around the world improve the products they sell, and the shops in which we buy them.\nSo what does Hakan do? In his spare time he is a secret shopper and product tester for a Turkish market research company called Twentify.\nTwentify has just 27 permanent employees, and has only been going for three years, but its global clients now include Coca-Cola, Danone, KFC, Philips, L'Oreal, Samsung, and Unilever."));
            Console.WriteLine();
            Console.WriteLine(">>>Test #1 =================================================");
            Console.WriteLine(JustifyText(40, "In the beginning God created the heavens and the earth. Now the earth was formless and empty, darkness was over the surface of the deep, and the Spirit of God was hovering over the waters.\nAnd God said, \"Let there be light, \" and there was light. God saw that the light was good, and he separated the light from the darkness. God called the light \"day, \" and the darkness he called \"night.\" And there was evening, and there was morning - the first day."));
            Console.WriteLine();
            Console.WriteLine(">>>Test #2 =================================================");
            Console.WriteLine(JustifyText(30, "a a a"));
            Console.WriteLine();
            Console.WriteLine(">>>Test #3 =================================================");
            Console.WriteLine(JustifyText(10, "This is a test with the longest word pneumonoultramicroscopicsilicovolcanoconiosis in English. This text will be ignored because it exceeds the max of 40 bytes per word."));
            Console.WriteLine();
            Console.WriteLine(">>>Test #4 =================================================");
            Console.WriteLine(JustifyText(10, "Test with the word \"constitution\", which is longer than the max, which is set to aaaaa."));
            Console.WriteLine();
            Console.WriteLine(">>>Test #5 =================================================");
            Console.WriteLine(JustifyText(40, "This is a paragraph test with multiple lines.\nLine number 1.\nLine number 2.\nThe end."));

            Console.WriteLine("***** the end *****");
        }

        /// <summary>
        /// This API formats an incoming text in "justify" mode, by inserting spaces between the words till it gets the max line length.
        /// Note: I personally dont think the last line of the paragraph has to be justified (e.g. MS Word does not do it), so I created
        /// a boolean parameter to control it.
        /// </summary>
        /// <param name="mxLineLength">represents the max length of the line.</param>
        /// <param name="incomingText">represents the text to be formatted in "justify" mode.</param>
        static string JustifyText(int mxLineLength, string incomingText) {

            StringBuilder incomingln = new StringBuilder();
            StringBuilder justifiedText = new StringBuilder();

            if (incomingText == null) {
                return null; // invalid incoming text, nothing to do.
            }

            // sets the default max length line.
            mxLineLength = (mxLineLength <= 0 ? LINE_DEF_LENGTH : (mxLineLength > LINE_MAX_LENGTH ? LINE_MAX_LENGTH : mxLineLength));
            string[] paragraphs = incomingText.Split('\n');

            string[] words = incomingText.Split(' ');
            for (int i=0; i<words.Length; i++) {    // verifies the longest word.
                int l = words[i].Length;
                if (l > mxLineLength) {
                    if (l > LINE_MAX_LENGTH) {
                        return null;                // there is a word, which is exceeds the max line length
                    }
                    mxLineLength = l;               // lets use the longest word lenght
                }
            }
            for (int p = 0; p < paragraphs.Length; p++) {   // firstly breaks the paragraphs
                words = paragraphs[p].Split(' ');           // breaks the words of each paragraph
                for (int w = 0; w < words.Length; w++) {
                    int lnLength = (incomingln.Length + words[w].Length);
                    if (lnLength <= mxLineLength) {
                        incomingln.Append(words[w]);
                        incomingln.Append(' ');
                    } else {
                        w--; // exceeded the max length per line, lets take it again in the next loop.
                    }
                    if ((lnLength > mxLineLength) || (w == words.Length - 1)) {
                        string _l = incomingln.ToString().Trim();
                        int diff = mxLineLength - _l.Length;
                        if ((diff > 0) && ((w != words.Length - 1) || (JUSTIFY_LAST_PARAGRAPH_LINE))) {
                            string[] strAWords = _l.Split(' ');
                            StringBuilder[] _sbWords = new StringBuilder[strAWords.Length];
                            for (int x = 0; (x < strAWords.Length) && (diff > 0); x++) {
                                _sbWords[x] = (new StringBuilder(strAWords[x])).Append(' ');
                            }
                            while ((diff > 0) && (strAWords.Length > 1)) {
                                for (int x = 0; (x < strAWords.Length - 1) && (diff > 0); x++) {
                                    _sbWords[x].Append(' '); // formats the line in justify mode
                                    diff--;
                                }
                            }
                            for (int x = 0; x < strAWords.Length; x++) { 
                                justifiedText.Append(_sbWords[x].ToString()); // inserts the formatted line into the full text
                            }
                        } else {
                            justifiedText.Append(_l); // no need to format.
                        }
                        if (w < words.Length - 1) {
                            justifiedText.Append('\n');
                        }
                        incomingln.Clear();
                    }
                }
                justifiedText.Append(incomingln);
                if (p < paragraphs.Length - 1) {
                    justifiedText.Append('\n');
                    justifiedText.Append('\n');
                }
            }
            return justifiedText.ToString();
        }
    }
}
