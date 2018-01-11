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
            {"War_Declare_1","{0} declared war on {1}" },
            {"War_Declare_2","{1} target of {0} aggression (War!)" },
            {"War_Declare_3","{0} menace declares war upon {1}" },
            {"War_Declare_4","Conflict Erupts between {0} and {1}" },
            {"War_Declare_5","{0} troops prepared for war against {1} menace" },
            {"War_Declare_Holy","{0} Has Declared a crusade for {1}!" },
            {"War_Declare_Revolt_0","Peasants rise in {1}" },
            {"War_Declare_Revolt_1","Revolutionaries have declared war upon {1}" },
            {"War_Declare_Revolt_2","Uprising in {1}!" },
            //War_Capital - captor {0}. defender {1}
            {"War_Capital","{0} Captures City Of {1}!" },
            //War_Peace - victor {0}, loser {1}
            {"War_Peace_0","{1} Surrenders to {0}" },
            {"War_Peace_1","{1} accepts {0} Peace Negotiations!" },
            {"War_Peace_2","{0} Conquers {1}!" },
            //Religion_Form - City {0}. Religion {1}
            {"Religion_Form_0", "{1} begins in {0}" },
            {"Religion_Form_1", "{1} Forms in city of {0}" },
            {"Religion_Form_2","{0} has formed {1}!" },
            //Religion_Convert - Country {0}. Religion {1}
            {"Religion_Convert", "{0} Has converted to {1}" },
            //Science_Increase {0} City. {1} Level.
            {"Science_Increase_0", "{0} Develops Level {1} Science"},
            {"Science_Increase_1", "{0} Science develops to Level {1}" },
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
            
        };
    }
}
