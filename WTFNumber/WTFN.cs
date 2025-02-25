namespace WTFNumber
{
    public class WTFN
    {
        private readonly string _resultFilePath;
        private readonly int _magicWTFNumber = 6174;

        public WTFN()
        {
            _resultFilePath = Path.Combine(Environment.CurrentDirectory, "result.txt");

            if (!File.Exists(_resultFilePath))
            {
                var file = File.Create(_resultFilePath);
                file.Close();
            }
        }

        public void DoWTF()
        {
            var number = 1000;

            while (number != 10000)
            {
                File.AppendAllText(_resultFilePath, $"Current number: {number}\n");

                WTFSum(number);

                number++;
            }
        }

        private void WTFSum(int number)
        {
            var sum = 0;
            var checkNum = number;

            while (sum != _magicWTFNumber)
            {
                var firstS = GetOrderedWTFNumber(checkNum, Order.DESC);
                var secondS = GetOrderedWTFNumber(checkNum, Order.ASC);

                checkNum = firstS - secondS;

                File.AppendAllText(_resultFilePath, $"\t{firstS} - {secondS} = {checkNum}\n");

                if (checkNum == 0)
                {
                    File.AppendAllText(_resultFilePath, "\tskip...\n");
                    return;
                }

                sum = checkNum;
            }

            if (sum == _magicWTFNumber)
            {
                File.AppendAllText(_resultFilePath, $"\tCorrect for {number}!\n");
            }
        }

        private int GetOrderedWTFNumber(int number, Order order)
        {
            var strNum = number.ToString();

            switch (order)
            {
                case Order.ASC:
                    {
                        var lowerNumStr = string.Join("", strNum.OrderBy(c => c));

                        while (lowerNumStr.Length < 4)
                        {
                            lowerNumStr = "0" + lowerNumStr;
                        }

                        return int.Parse(lowerNumStr);
                    }
                case Order.DESC:
                    {
                        var biggerNumStr = string.Join("", strNum.OrderByDescending(c => c));

                        while (biggerNumStr.Length < 4)
                        {
                            biggerNumStr += "0";
                        }

                        return int.Parse(biggerNumStr);
                    }
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
