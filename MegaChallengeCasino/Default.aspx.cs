using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MegaChallengeCasino
{
    public partial class Default : System.Web.UI.Page
    {
        // The random class is created as a variable in order to make the spin reel random for each image.  
        Random random = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void leverBtn_Click(object sender, EventArgs e)
        {
            int bet = 0;
            if (!int.TryParse(betTextBox.Text, out bet)) return;
            int winnings = imagesValue(bet);
            displayResult(bet, winnings);
        }

        
        private int imagesValue(int bet)
        {
            string[] reelValue = new string[] {spinReel(), spinReel(), spinReel()};
            displayImages(reelValue);
            int multiplier = evaluateMultiplyer(reelValue);
            return bet * multiplier;
        }

        

        private void displayImages(string[] reelValue)
        {
            leftImage.ImageUrl = "/images/" + reelValue[0] + ".png";
            midelImage.ImageUrl = "/images/" + reelValue[1] + ".png";
            rightImage.ImageUrl = "/images/" + reelValue[2] + ".png";
        }
        
       
        // This method will be used for each image in the reel to randomiz it. 
        private string spinReel()
        {
            string[] images = new string[12] {"Bar", "Bell", "Cherry", "Clover", "Diamond",
                "HorseShoe", "Lemon", "Orange", "Plum", "Seven", "Strawberry", "Watermellon"};
            return images[random.Next(11)];
        }

        private int evaluateMultiplyer(string[] reelValue)
        {
            // if 1 Bar is present then return 0
            if (BarPresent(reelValue)) return 0;

            // if 3 seven is present then return 100
            if (jackpot(reelValue)) return 100;

            // if cherries are present return 2 or 3 or 4
            int multiplier = 0;
            if (ifWinner(reelValue, out multiplier)) return multiplier;

            // if non of the above returns a value then return a zero for this method 
            return 0;
        }

        

        private bool BarPresent(string[] reelValue)
        {
            if (reelValue[0] == "Bar" || reelValue[1] == "Bar" || reelValue[2] == "Bar")
                return true;
            else
                return false;
        }

        private bool jackpot(string[] reelValue)
        {
            if (reelValue[0] == "Seven" && reelValue[1] == "Seven" && reelValue[2] == "Seven")
                return true;
            else
                return false;
        }
      

        // This method sets the multiplier to the return value of the CherryMultiplier method
        // and if the multiplier returns 0 then the method will return false 
        private bool ifWinner(string[] reelValue, out int multiplier)
        {
            multiplier = cherryMultiplier(reelValue);
            if (multiplier > 0) return true;
            return false;
        }


        // determain the cherry multiplier by taking the cherry count from the 
        // determainCherryCount method and figuring out the multiplier. 
        private int cherryMultiplier(string[] reelValue)
        {
            int cherryCount = determianCherryCount(reelValue);
            if (cherryCount == 1) return 2;
            if (cherryCount == 2) return 3;
            if (cherryCount == 3) return 4;
            return 0;
        }


        // determain how many cherries are in the spin Reel and for each cherry add one to the cherry count.
        private int determianCherryCount(string[] reelValue)
        {
            int cherryCount = 0;
            if (reelValue[0] == "Cherry") cherryCount++;
            if (reelValue[1] == "Cherry") cherryCount++;
            if (reelValue[2] == "Cherry") cherryCount++;
            return cherryCount;
        }


        // This method determains the message that will display in the resultLable if the player wins or losses. 
        private void displayResult(int bet, int winnings)
        {
            if (winnings > 0)
            {
                resultLabel.Text = string.Format("You bet {0:C} and won {1:C}!", bet, winnings);
            }
            else
            {
                resultLabel.Text = string.Format("Sorry, you lost {0:C}. Better luck next time.", bet);
            }
        }

    }
}