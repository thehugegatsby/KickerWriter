namespace KickerWriter.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class FileContentParser
    {
        private string _topicString;

        public List<Tuple<string, string>> GetStringTupleListFromFileContent(
            IEnumerable<string> fileContent,
            string fileName)
        {
            List<Tuple<string, string>> fileContentTupleList = new List<Tuple<string, string>>();
            foreach (string line in fileContent)
            {
                string trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine))
                {
                    continue;
                }
                else if (IsTopicString(trimmedLine))
                {
                    this._topicString = trimmedLine.Substring(1, trimmedLine.Length - 2).Trim();
                }
                else if (ContaintsKeyValuePair(trimmedLine))
                {
                    this.FillStringListsWithInformationOfLine(fileContentTupleList, trimmedLine, fileName);
                }
            }

            return fileContentTupleList;
        }

        private static bool IsTopicString(string line)
        {
            if (line.Length > 2 && line[0] == '-' && line[line.Length - 1] == ':')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool ContaintsKeyValuePair(string line)
        {
            if (line.Contains(":") && line[0] != '-')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static string ChangeKeyWords(string oldKeyWord)
        {
            foreach (Tuple<string, string> tuple in GlobalData.TopicStringReplaceList)
            {
                if (oldKeyWord == tuple.Item1)
                {
                    return tuple.Item2;
                }
            }

            return oldKeyWord;
        }

        private void FillStringListsWithInformationOfLine(
            ICollection<Tuple<string, string>> collectionFileContentTuple,
            string line,
            string fileName)
        {
            string[] splittedLine = line.Split(new char[] { ':' }, 2);

            string keyWord = splittedLine[0].Trim();
            keyWord = ChangeKeyWords(keyWord);
            string value = splittedLine[1].Trim();
            collectionFileContentTuple.Add(Tuple.Create(fileName + "_" + this._topicString + "_" + keyWord, value));
        }
    }
}