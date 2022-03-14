namespace WpfApplication1
{
    class Player
    {
        
            public Player(string name, int point)
            {
                Name = name;
                Point = point;
            }
            public string Name
            {
                get;
            }

            public int Point
            {
                get;
            }
        public override string ToString()
        {
            string answer = "";
            answer += Name.ToString() + "\t" + Point;
            return answer;
        }
    }
    
}
