using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Mvc.Models
{
    public class StateModel
    {
        public string State { get; set; }
        public string Abbreviation { get; set; }
        public int Statehood { get; set; }
        public string Capital { get; set; }
        public double LandArea { get; set; }
        public int Population { get; set; }

        private static readonly Random r = new Random();
        private static readonly object syncLock = new object();
        private DateTime theDate;
        public DateTime TheDate
        {
            get
            {
                if (theDate == DateTime.MinValue)
                {
                    lock(syncLock)
                    {
                        int month = r.Next(1, 12);
                        int day = r.Next(1, 28);
                        int hour = r.Next(0, 23);
                        int minute = r.Next(0, 59);
                        int second = r.Next(0, 59);
                        theDate = new DateTime(Statehood, month, day, hour, minute, second);
                    }
                }
                return theDate;
            }
        }

        public StateModel()
        {
        }

        public static List<StateModel> GetStates()
        {
            return new List<StateModel>() {
                new StateModel() { State = "Alabama", Abbreviation = "AL", Statehood = 1819, Capital = "Montgomery", LandArea = 155.4, Population = 580300 },
                new StateModel() { State = "Alaska", Abbreviation = "AK", Statehood = 1959, Capital = "Juneau", LandArea = 2716.7, Population = 31275 },
                new StateModel() { State = "Arizona", Abbreviation = "AZ", Statehood = 1912, Capital = "Phoenix", LandArea = 474.9, Population = 5638519 },
                new StateModel() { State = "Arkansas", Abbreviation = "AR", Statehood = 1836, Capital = "Little Rock", LandArea = 116.2, Population = 1070615 },
                new StateModel() { State = "California", Abbreviation = "CA", Statehood = 1850, Capital = "Sacramento", LandArea = 97.2, Population = 2993611 },
                new StateModel() { State = "Colorado", Abbreviation = "CO", Statehood = 1876, Capital = "Denver", LandArea = 153.4, Population = 3152353 },
                new StateModel() { State = "Connecticut", Abbreviation = "CT", Statehood = 1788, Capital = "Hartford", LandArea = 17.3, Population = 1336893 },
                new StateModel() { State = "Delaware", Abbreviation = "DE", Statehood = 1787, Capital = "Dover", LandArea = 22.4, Population = 198357 },
                new StateModel() { State = "Florida", Abbreviation = "FL", Statehood = 1845, Capital = "Tallahassee", LandArea = 95.7, Population = 548825 },
                new StateModel() { State = "Georgia", Abbreviation = "GA", Statehood = 1788, Capital = "Atlanta", LandArea = 131.7, Population = 5688863 },
                new StateModel() { State = "Hawaii", Abbreviation = "HI", Statehood = 1959, Capital = "Honolulu", LandArea = 85.7, Population = 1290463 },
                new StateModel() { State = "Idaho", Abbreviation = "ID", Statehood = 1890, Capital = "Boise", LandArea = 63.8, Population = 822232 },
                new StateModel() { State = "Illinois", Abbreviation = "IL", Statehood = 1818, Capital = "Springfield", LandArea = 54, Population = 324432 },
                new StateModel() { State = "Indiana", Abbreviation = "IN", Statehood = 1816, Capital = "Indianapolis", LandArea = 361.5, Population = 2585939 },
                new StateModel() { State = "Iowa", Abbreviation = "IA", Statehood = 1846, Capital = "Des Moines", LandArea = 75.8, Population = 783688 },
                new StateModel() { State = "Kansas", Abbreviation = "KS", Statehood = 1861, Capital = "Topeka", LandArea = 56, Population = 358297 },
                new StateModel() { State = "Kentucky", Abbreviation = "KY", Statehood = 1792, Capital = "Frankfort", LandArea = 14.7, Population = 96285 },
                new StateModel() { State = "Louisiana", Abbreviation = "LA", Statehood = 1812, Capital = "Baton Rouge", LandArea = 76.8, Population = 1032037 },
                new StateModel() { State = "Maine", Abbreviation = "ME", Statehood = 1820, Capital = "Augusta", LandArea = 55.4, Population = 136250 },
                new StateModel() { State = "Maryland", Abbreviation = "MD", Statehood = 1788, Capital = "Annapolis", LandArea = 6.73, Population = 38394 },
                new StateModel() { State = "Massachusetts", Abbreviation = "MA", Statehood = 1788, Capital = "Boston", LandArea = 48.4, Population = 5140452 },
                new StateModel() { State = "Michigan", Abbreviation = "MI", Statehood = 1837, Capital = "Lansing", LandArea = 35, Population = 578333 },
                new StateModel() { State = "Minnesota", Abbreviation = "MN", Statehood = 1858, Capital = "Saint Paul", LandArea = 52.8, Population = 3787959 },
                new StateModel() { State = "Mississippi", Abbreviation = "MS", Statehood = 1817, Capital = "Jackson", LandArea = 104.9, Population = 712571 },
                new StateModel() { State = "Missouri", Abbreviation = "MO", Statehood = 1821, Capital = "Jefferson City", LandArea = 27.3, Population = 192886 },
                new StateModel() { State = "Montana", Abbreviation = "MT", Statehood = 1889, Capital = "Helena", LandArea = 14, Population = 102991 },
                new StateModel() { State = "Nebraska", Abbreviation = "NE", Statehood = 1867, Capital = "Lincoln", LandArea = 74.6, Population = 560536 },
                new StateModel() { State = "Nevada", Abbreviation = "NV", Statehood = 1864, Capital = "Carson City", LandArea = 143.4, Population = 55274 },
                new StateModel() { State = "New Hampshire", Abbreviation = "NH", Statehood = 1788, Capital = "Concord", LandArea = 64.3, Population = 42695 },
                new StateModel() { State = "New Jersey", Abbreviation = "NJ", Statehood = 1787, Capital = "Trenton", LandArea = 7.66, Population = 451426 },
                new StateModel() { State = "New Mexico", Abbreviation = "NM", Statehood = 1912, Capital = "Santa Fe", LandArea = 37.3, Population = 259496 },
                new StateModel() { State = "New York", Abbreviation = "NY", Statehood = 1788, Capital = "Albany", LandArea = 21.4, Population = 955448 },
                new StateModel() { State = "North Carolina", Abbreviation = "NC", Statehood = 1789, Capital = "Raleigh", LandArea = 114.6, Population = 1534382 },
                new StateModel() { State = "North Dakota", Abbreviation = "ND", Statehood = 1889, Capital = "Bismarck", LandArea = 26.9, Population = 170051 },
                new StateModel() { State = "Ohio", Abbreviation = "OH", Statehood = 1803, Capital = "Columbus", LandArea = 210.3, Population = 2789619 },
                new StateModel() { State = "Oklahoma", Abbreviation = "OK", Statehood = 1907, Capital = "Oklahoma City", LandArea = 607, Population = 1832987 },
                new StateModel() { State = "Oregon", Abbreviation = "OR", Statehood = 1859, Capital = "Salem", LandArea = 45.7, Population = 545375 },
                new StateModel() { State = "Pennsylvania", Abbreviation = "PA", Statehood = 1787, Capital = "Harrisburg", LandArea = 8.11, Population = 696918 },
                new StateModel() { State = "Rhode Island", Abbreviation = "RI", Statehood = 1790, Capital = "Providence", LandArea = 18.5, Population = 1808998 },
                new StateModel() { State = "South Carolina", Abbreviation = "SC", Statehood = 1788, Capital = "Columbia", LandArea = 125.2, Population = 1045483 },
                new StateModel() { State = "South Dakota", Abbreviation = "SD", Statehood = 1889, Capital = "Pierre", LandArea = 13, Population = 13646 },
                new StateModel() { State = "Tennessee", Abbreviation = "TN", Statehood = 1796, Capital = "Nashville", LandArea = 473.3, Population = 2217974 },
                new StateModel() { State = "Texas", Abbreviation = "TX", Statehood = 1845, Capital = "Austin", LandArea = 251.5, Population = 2506681 },
                new StateModel() { State = "Utah", Abbreviation = "UT", Statehood = 1896, Capital = "Salt Lake City", LandArea = 109.1, Population = 1310637 },
                new StateModel() { State = "Vermont", Abbreviation = "VT", Statehood = 1791, Capital = "Montpelier", LandArea = 10.2, Population = 7855 },
                new StateModel() { State = "Virginia", Abbreviation = "VA", Statehood = 1788, Capital = "Richmond", LandArea = 60.1, Population = 1435889 },
                new StateModel() { State = "Washington", Abbreviation = "WA", Statehood = 1889, Capital = "Olympia", LandArea = 16.7, Population = 281148 },
                new StateModel() { State = "West Virginia", Abbreviation = "WV", Statehood = 1863, Capital = "Charleston", LandArea = 31.6, Population = 355614 },
                new StateModel() { State = "Wisconsin", Abbreviation = "WI", Statehood = 1848, Capital = "Madison", LandArea = 68.7, Population = 794714 },
                new StateModel() { State = "Wyoming", Abbreviation = "WY", Statehood = 1890, Capital = "Cheyenne", LandArea = 21.1, Population = 151204 }
            };
        }
    }

    public class TheTestModel
    {
        public int ID { get; set; }
        public string TheTest { get; set; }
    }
}