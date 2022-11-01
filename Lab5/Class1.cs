using System;
using System.Collections.Generic;
using System.IO;
namespace Lab5
{
    internal class MyShop
    {

        private static bool isShop = true;
        private static string shopName = "Ubisoft";
        private static string playerInput;
        private static List<string> shopItems = new List<string>();
        private static List<string> playerInventory = new List<string>();

        static readonly string playerInventoryFile = @".\playerInventory.txt";
        static readonly string storeInventoryFile = @".\storeInventory.txt"; //D:\Visual Studio\Lab5\Lab5\bin\release\storeInventory.txt

        public MyShop()
        {
            fillShop();
            readInventory();
        }
        private static void fillShop()
        {
            readFile(storeInventoryFile);
        }
        private void readInventory()
        {
            readFile(playerInventoryFile);
        }
        public void Store()
        {
            Console.WriteLine($"Hello and welcome to {shopName}\nType 'Help' to show a list of all commands!");

            while (isShop)
            {
                playerInput = Console.ReadLine();
                playerInput = playerInput.ToLower();
                if (playerInput == null || playerInput.Equals(""))
                {
                    Console.WriteLine("Please enter text!");
                }
                else if (playerInput.Equals("hi") || playerInput.Equals("hello"))
                {
                    Console.WriteLine($"Hello there! Welcome to {shopName}! Please input the item you're looking for!");
                }
                else if (playerInput.Contains("do you have") || playerInput.Contains("is the item") || playerInput.Contains("item"))
                {
                    if (CheckAvailability(playerInput))//means the game is available
                    {
                        Random r = new Random();
                        Console.WriteLine($"Yes, this item is available! The cost is {r.Next(100,1001)}!\nAlso there are this items available!");

                    }
                    else
                    {
                        Console.WriteLine("Unfortunately this item is not available at the time, check back later!\nBut there are this items available!");
                    }
                    printList(shopItems);
                }
                else if (playerInput.Contains("0") || playerInput.Contains("1") || playerInput.Contains("2") || playerInput.Contains("2") || playerInput.Contains("3") || playerInput.Contains("4") || playerInput.Contains("5") || playerInput.Contains("6") || playerInput.Contains("7") || playerInput.Contains("8") || playerInput.Contains("9"))
                {
                    buyItemByNumber(returnOnlyInt(playerInput));
                }
                else if (playerInput.Contains("buy") || playerInput.Contains("purchase"))
                {
                    Console.WriteLine($"Please type the number from the item you want to buy!"); //The cost is {price.ToString()}!");
                }
                else if (playerInput.Contains("show") || playerInput.Contains("what") || playerInput.Contains("available"))
                {
                    printList(shopItems);
                }
                else if (playerInput.Contains("section"))
                {
                    Console.WriteLine("To view sections please enter section ex: Section Weapons\nAvailable sections:\nWeapons\nApparel");
                    if (playerInput.Contains("weapon"))
                    {
                        printSection("weapon");
                    }
                    else if (playerInput.Contains("apparel"))
                    {
                        printSection("apparel");
                    }
                    else
                    {
                        Console.WriteLine("Section was not found!");
                    }

                }
                else if (playerInput.Contains("refill"))
                {
                    refill(playerInput);
                      //readFile(playerInventoryFile);
                    //readFile(storeInventoryFile);
                }
                //else if (playerInput.Contains("write"))//for testing write
               // {
                //    writeFiles();
               // }

                else if (playerInput.Contains("inventory") || playerInput.Contains("my") || playerInput.Contains("player") || playerInput.Contains("list"))
                {
                    Console.WriteLine("The player currently has:");
                    printList(playerInventory);
                }
                else if (playerInput.Contains("exit") || playerInput.Contains("leave") || playerInput.Contains("bye"))
                {
                    Console.WriteLine("Come back later!");
                    System.Threading.Thread.Sleep(3000);
                    isShop = false;
                }
                else if (playerInput.Contains("help") )
                {
                    Console.WriteLine("The available commands are:\n" +
                        "Hi/Hello = Will greet player\nDo you have/Is this + item availabe = will show availability & price of that item\n" +
                        "Buy + numberOfitem = Will buy item in that position\nShow = will show all available items\n" +
                        "Section + section will show only items in that section\nInventory = will show player inventory\n" +
                        "Exit/Leave = will quit the store\n\nTo refill use refill + the item you wish to add to store ex: refill Iron Sword\tWeapon" +
                        "\nMake sure to refill to add tab so ''Iron Sword(tab)Weapon''");
                }
                else
                {
                    Console.WriteLine("Please enter a valid command!");
                }
            }

        }
        public static void refill(string input)
        {
            if (!input.Contains("\t"))
            {
                Console.WriteLine("Please enter a valid form!");
                return;
            }
            input = input.Substring(input.IndexOf(' ')+1);
            shopItems.Add(input);
            writeFiles();
        }
        private static bool CheckAvailability(string item)
        {
            item.ToLower();
            foreach (string tempString in shopItems)
            {                            
                string temp=tempString.Substring(0, tempString.IndexOf('\t'));
                temp=temp.ToLower();
                if (item.Contains(temp))
                {
                    return true;
                }
            }
            return false;
        }
        private static void printSection(string section)
        {
            section.ToLower();
            foreach (string tempString in shopItems)
            {
                if (tempString.ToLower().Contains(section))
                {
                    Console.WriteLine(tempString);
                }
            }
        }

        private static int returnOnlyInt(string temp)
        {
            string final = "";
            foreach (char c in temp)
            {
                if (c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9')
                {
                    final += c;
                }
            }
            return int.Parse(final);
        }
        private static void buyItemByNumber(int numberToBuy)
        {
            if (numberToBuy > shopItems.Count-1)
            {
                Console.WriteLine("Please enter a valid number to buy!");
                return;
            }
            Console.WriteLine("You have bought " + shopItems[numberToBuy]);
            playerInventory.Add(shopItems[numberToBuy]);
            shopItems.Remove(shopItems[numberToBuy]);
            writeFiles();
        }
        private static void printList(List<string> toPrint)
        {
            int i = 0;
            foreach (string tempString in toPrint)
            {
                Console.WriteLine(i + " " + tempString);
                i++;
            }
        }
        private static void readFile(string textFile)
        {
            if (File.Exists(textFile))
            {
                // Read file using StreamReader. Reads file line by line  
                using (StreamReader file = new StreamReader(textFile))
                {
                    string ln;

                    while ((ln = file.ReadLine()) != null)
                    {
                        if (textFile.Contains("store"))
                        {
                            shopItems.Add(ln);
                        }
                        if (textFile.Contains("player"))
                        {
                            playerInventory.Add(ln);
                        }
                        //Console.WriteLine(ln);
                    }
                    file.Close();
                }
            }
        }
        private static string getListAsString(List<string> tempList)
        {
            string s = "";
            foreach(string temp in tempList)
            {
                s += temp+"\n";
            }
            return s;
        }
        private static void writeFiles()
        {
            File.WriteAllText(playerInventoryFile, getListAsString(playerInventory));
            File.WriteAllText(storeInventoryFile, getListAsString(shopItems));
        }

    }

}

