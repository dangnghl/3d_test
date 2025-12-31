public record InputPair(double UpDuration = 0, double DownDuration = 0)
    {
        public double UpDuration { get; set; } = UpDuration;
        public double DownDuration { get; set;} = DownDuration;
        public override string ToString()
        {
            return $"[(Up,{UpDuration}),(Down,{DownDuration})]";
        }

        public void OnActive(bool selectDown,double delta)
        {
            if(selectDown)
                DownDuration += delta;
            else
                UpDuration += delta;
        }
    }
