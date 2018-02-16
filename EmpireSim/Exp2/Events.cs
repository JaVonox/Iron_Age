using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exp2
{
    class Events
    {
        public bool ContainsKey(string key) { return dictionaryevents.ContainsKey(key); }
        public string GetValue(string key) { return dictionaryevents[key]; }

        static Dictionary<string, string> dictionaryevents = new Dictionary<string, string>
        {
            //War_Declare - aggressor {0}. defender {1}
            {"War_Declare_0","{0} has declared war on {1}" },
            //War_Peace - victor {0}, loser {1}
            {"War_Peace_0","{1} Made peace with {0}" },
            //Religion_Form - City {0}. Religion {1}
            {"Religion_Form_0", "{1} begins in {0}" },
            {"Religion_Form_1", "{1} Forms in city of {0}" },
            {"Religion_Form_2","{0} has formed {1}!" },
            //Nation_Rank - Target {0}. Rank {1}
            {"Nation_Rank","{0} Has become a{1}!" },
            //Religion_Convert - Country {0}. Religion {1}
            {"Religion_Convert", "{0} Has converted to {1}" },
            //Science_Increase {0} City. {1} Level.
            {"Science_Increase_0", "{0} Develops Level {1} Science"},
            {"Science_Increase_1", "{0} Gained Level {1} Science" },
            {"Science_Increase_2", "Level {1} Science discovered in {0}"},
            //Science_Milestone. {0} City. {1} Year
            {"Science_Milestone_Iron", "{1} - {0} Discovered Iron!" }, //50
            {"Science_Milestone_Steel", "{1} - {0} Forges Steel!" }, //100
            {"Science_Milestone_Gunpowder", "{1} - {0} Invents Gunpowder!" }, //150
            {"Science_Milestone_Oil", "{1} - {0} Has Discovered Oil Technology!" }, //200
            //Science_Age. Stone (0 - 10), Ancient (11 - 30), Bronze (31 - 40), Dark (41 - 49), Iron (50 - 70), Classical (71, 80), Early Middle (81,99), High Middle (100,120), Medieval (121, 139), Late Middle (140,149), Renaissance (150, 165), Early Modern (166, 170), Revolutionary (171,185), Modern (186 - 200), Information (200+)
            //{"Science_Age_" }
            //Country Advance
            //Save
            {"Save","Game has been saved" },
            //New Year. Year {0}
            {"New_Year", "It is now the year {0}" },
            //Reinforce.
            {"Reinforce", "New troops have arrived" },
            
        };
    }
}
