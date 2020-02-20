using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOFT_152_AIR_BnB
{
    class District
    {
        private string districtName;
        private int numNeighbourhoods;
        private Neighbourhood[] neighbourhoods;
        private double avgPrice;
        private bool readingData;
        public District(string inDistrictName, string inNumNeighbourhoods)
        {
            SetDistrictName(inDistrictName);
            SetNumNeighbourhoods(inNumNeighbourhoods);
            neighbourhoods = new Neighbourhood[GetNumNeighbourhoods()];
            //Reading data always true on this constructor
            readingData = true;
        }
        //Constructor for the user creating a new district
        public District(string inDistrictName, string inNumNeighbourhoods, Neighbourhood inNeighbourhood)
        {
            SetDistrictName(inDistrictName);
            SetNumNeighbourhoods(inNumNeighbourhoods);
            neighbourhoods = new Neighbourhood[] { inNeighbourhood };
        }
        public void AddNeighbourhood(Neighbourhood inNeighbourhood, int index)
        {
            neighbourhoods[index] = inNeighbourhood;
            if (!readingData)
            {
                //Recalc avg
                CalculateAverage();
            }
        }
        public void AppendNeighbourhood(Neighbourhood inNeighbourhood)
        {
            //Make the array 1 larger, place the new neighbourhood at the end and then recalculate the average price
            Array.Resize(ref neighbourhoods, neighbourhoods.Length + 1);
            neighbourhoods[neighbourhoods.Length - 1] = inNeighbourhood;
            CalculateAverage();
        }
        public void CalculateAverage()
        {
            double avg = 0;
            foreach (Neighbourhood nbHood in neighbourhoods)
            {
                avg += nbHood.GetAvgPrice();
            }
            avgPrice = avg / numNeighbourhoods; 
        }
        public int GetArrLength()
        {
            return neighbourhoods.Length;
        }
        public void ReadingDone()
        {
            readingData = false;
        }

        //Gets & sets - opening a message box in catch to give the user better information
        //String overloads for numbers - user input and text file will use these
        public double GetAvgPrice()
        {
            return avgPrice;
        }
        public void SetDistrictName(string inName)
        {
            districtName = inName;
        }
        public string GetDistrictName()
        {
            return districtName;
        }

        public void SetNumNeighbourhoods(int inNum)
        {
            numNeighbourhoods = inNum;
        }
        public void SetNumNeighbourhoods(string inNum)
        {
            try
            {
                numNeighbourhoods = Convert.ToInt32(inNum);
            }
            catch (Exception)
            {
                throw new Exception(String.Format("Error processing number of neighbourhoods ({0}) for district {1}", inNum, districtName));
            }
        }
        public int GetNumNeighbourhoods()
        {
            return numNeighbourhoods;
        }

        //
        //Get & set neighbourhood
        //
        public void SetNeighbourHood(Neighbourhood value, int index)
        {
            neighbourhoods[index] = value;
        }
        public Neighbourhood GetNeighbourhood(int index)
        {
            try
            {
                //Some ttimes a race error can be made here, but it will be re-loaded at those times
                return neighbourhoods[index];
            }
            catch
            {
                return null;
            }
        }
        public Neighbourhood[] GetAllNeighbourhoods()
        {
            return neighbourhoods;
        }
    }
}
