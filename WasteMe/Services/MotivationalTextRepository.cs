using System;
using System.Collections.Generic;

namespace WasteMe.Services
{
    public class MotivationalTextRepository : IMotivationalTextsRepository
    {
        public string GetRandomTextForWasteType(string wasteType)
        {
            if (!texts.ContainsKey(wasteType))
            {
                return string.Empty;
            }

            var randomIndex = new Random().Next(0, texts[wasteType].Length - 1);
            return texts[wasteType][randomIndex];
        }

        private Dictionary<string, string[]> texts
            = new Dictionary<string, string[]>
            {
                { "metal plastic", new [] {
                    "Recycling plastic takes 88% less energy than making plastic from raw materials.",
                    "Enough plastic is thrown away each year to circle the Earth four times.",
                    "If we recycled the other 75% we could save 1 billion gallons of oil and 44 million cubic yards of landfill space annually.",
                    "Did you know that just by using recycled steel there is an 86 percent reduction in air pollution and a 76 percent reduction in water pollution?",
                    "In a landfill, aluminum and tin cans will stick around for decades. It can take as long as 50 years for a steel food can to decompose, and as long as 200 years for aluminum to break down.",
                    "Recycling aluminum uses 90 percent less energy than making primary-production (virgin) aluminum!"} },
                { "paper", new[]
                {
                    "To produce each week's Sunday newspapers, 500,000 trees must be cut down.",
                    "If all our newspaper was recycled, we could save about 250,000,000 trees each year!",
                    "Approximately 1 billion trees worth of paper are thrown away every year in the U.S."
                }}
            };
    }
}
