using System;
using System.Windows.Forms;

namespace SOFT_152_AIR_BnB
{
    class Data
    {
        private District[] districts;
        public Data()
        {
            districts = new District[0];
        }
        public void AddDistrict(District inDistrict)
        {
            //Resize the district array then put the new district in the end
            Array.Resize(ref districts, districts.Length + 1);
            districts[districts.Length - 1] = inDistrict;
        }
        public District GetDistrict(int index)
        {
            return districts[index];
        }
        //Looping through every item in a depth-first style search
        //Returning the index that a match has been found at
        //Will only return the first search term, but everything in the datafile is unique
        public int[] SearchByName(string searchName)
        {
            for (int i = 0; i < districts.Length; i++)
            {
                if (districts[i].GetDistrictName().ToLower() == searchName)
                {
                    return new int[] { i };
                }
                for (int j = 0; j < districts[i].GetArrLength(); j++)
                {
                    if (districts[i].GetNeighbourhood(j).GetNeighbourhoodName().ToLower() == searchName)
                    {
                        return new int[] { i, j };
                    }
                    for (int k = 0; k < districts[i].GetNeighbourhood(j).GetNumProperties(); k++)
                    {
                        if (districts[i].GetNeighbourhood(j).GetProperty(k).GetPropertyName().ToLower() == searchName)
                        {
                            return new int[] { i, j, k };
                        }
                        if (districts[i].GetNeighbourhood(j).GetProperty(k).GetPropertyID().ToString().ToLower() == searchName)
                        {
                            return new int[] { i, j, k };
                        }
                    }
                }
            }
            //Returns -1 if nothing is found, a message is shown from where this is called from
            return new int[] { -1 };
        }
        public District[] GetAllDistricts()
        {
            return districts;
        }
    }
}
