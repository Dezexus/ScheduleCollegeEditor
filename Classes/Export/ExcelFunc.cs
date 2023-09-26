namespace ScheduleCollegeEditor.Classes.Export
{
    /// <summary>
    /// Содержит методы для работы с excel
    /// </summary>
    class ExcelFunc
    {

        /// <summary>
        /// Выдаёт адрес столбца, по его номеру
        /// </summary>
        public static string GetColumnLetter(int colIndex) {

            int div = colIndex;
            string colLetter = "";
            int mod = 0;

            while (div > 0) {

                mod = (div - 1) % 26;
                colLetter = (char)(65 + mod) + colLetter;
                div = (int)((div - mod) / 26);
            }
            return colLetter;
        }

    }
}
