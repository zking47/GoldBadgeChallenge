using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoldBadgeChallenge.Data.Entities;
using GoldBadgeChallenge.Repository;

namespace GoldBadgeChallenge.UI
{
    public class ProgramUI
    {
        private readonly DeliveryRepository _dRepo = new DeliveryRepository();

        public ProgramUI(){}

        public void Run()
        {
            RunApplication();
        }

        private void RunApplication()
        {
            bool isRunning = true;

            while(isRunning)
            {
                Console.Clear();

                Console.WriteLine("Enter the number of the option you wish to select:\n"+
                                  "1.  Show all info\n"+
                                  "2.  Show select info\n"+
                                  "3.  Create new delivery\n"+
                                  "4.  Remove delivery from database\n"+
                                  "5.  Update existing delivery\n"+
                                  "6.  Close Application\n");

                string userInput = Console.ReadLine()!;

                switch (userInput)
                {
                    case "1":
                    ShowAllInfo();
                    break;
                case "2":
                    ShowInfoByDeliveryStatus();
                    break;
                case "3":
                    CreateNewDelivery();
                    break;
                case "4":
                    RemoveInfoFromList();
                    break;
                    case "5":
                    UpdateExistingDelivery();
                    break;
                case "6":
                    isRunning = CloseApplication();
                    break;
                default:
                    System.Console.WriteLine("Sorry, Invalid selection, please try again.");
                    break;
                }
            }
        }
        private void UpdateExistingDelivery()
        {
            Console.Clear();
            Console.WriteLine("Enter Id:");
            int userInput = Console.Read();

            Delivery info = _dRepo.GetDeliveryById(userInput);
            if (info!=null)
            {
                Delivery updatedData = AddDeliveryDetails();
                if(_dRepo.UpdateExistingDelivery(info.Id,updatedData))
                {
                    System.Console.WriteLine("Success");
                }
                else
                {
                    System.Console.WriteLine("Failure");
                }
            }
            else
            {
                System.Console.WriteLine("Invalid Id. Could not find results.");
            }
            PressAnyKey();
        }
        
        private void ShowAllInfo()
        {
            Console.Clear();
            List<Delivery> listOfInfo = _dRepo.GetAllInfo();

            if (listOfInfo.Count > 0)
            {
                foreach(Delivery info in listOfInfo)
                {
                    ShowDeliveryInfoDetails(info);
                }
            }
            else
            {
                System.Console.WriteLine("No information available.");
            }
        }

        private void ShowDeliveryInfoDetails(Delivery info)
        {
            System.Console.WriteLine($"Customer Id: {info.CustomerId}\n"+
                                     $"Order Date: {info.OrderDate}\n"+
                                     $"Order Status: {info.OrderStatus}\n"+
                                     $"Delivery Date: {info.DeliveryDate}\n"+
                                     $"Item Number: {info.ItemNumber}\n"+
                                     $"Item Quantity: {info.ItemQuantity}\n"+
                                     "----------------------------------------------");
        }

        private void ShowInfoByDeliveryStatus()
        {
            Console.Clear();
            System.Console.WriteLine("Enter the delivery status you like to filter results by.");

            int userInput = int.Parse(Console.ReadLine()!);

            Delivery info = _dRepo.GetDeliveryById(userInput);

            if (info != null)
            {
                ShowDeliveryInfoDetails(info);
            }
            else
            {
                System.Console.WriteLine("Invalid");
            }
            PressAnyKey();
        }

        private void CreateNewDelivery()
        {
            Console.Clear();

            Delivery info = AddDeliveryDetails();

            if (_dRepo.InfoToDb(info))
            {
                System.Console.WriteLine("Added to DataBase.");
            }
            else
            {
                System.Console.WriteLine("Failed to add to DataBase.");
            }
            PressAnyKey();
        }

        private Delivery AddDeliveryDetails()
        {
            Console.Clear();

            Delivery info = new Delivery();

            System.Console.WriteLine("Please input Customer Id:");
            int userInputCustomerId = int.Parse(Console.ReadLine()!);
            info.CustomerId = userInputCustomerId;

            System.Console.WriteLine("Please input OrderDate YY/MM/DD:");
            DateTime userInputOrderDate = DateTime.Parse(Console.ReadLine()!);
            info.OrderDate = userInputOrderDate;

            System.Console.WriteLine("Please enter the OrderStatus:\n"+
                                     "1.  Scheduled\n"+
                                     "2.  EnRoute\n"+
                                     "3.  Delivered\n"+
                                     "4.  Canceled\n");
            string orderStatus = Console.ReadLine()!;
            switch (orderStatus)
            {
                case "1":
                    info.OrderStatus = Data.Entities.Enums.OrderStatus.Scheduled;
                    break;
                case "2":
                    info.OrderStatus = Data.Entities.Enums.OrderStatus.EnRoute;
                    break;
                case "3":
                    info.OrderStatus = Data.Entities.Enums.OrderStatus.Delivered;
                    break;
                case "4":
                    info.OrderStatus = Data.Entities.Enums.OrderStatus.Canceled;
                    break;
            }

            Console.WriteLine("Please input DeliveryDate YY/MM/DD:");
            DateTime userInputDeliveryDate = DateTime.Parse(Console.ReadLine()!);
            info.DeliveryDate = userInputDeliveryDate;

            System.Console.WriteLine("Please Input Item Number:");
            int userinputItemNumber = int.Parse(Console.ReadLine()!);
            info.ItemNumber = userinputItemNumber;

            System.Console.WriteLine("Please Input Item Quanitity:");
            int userInputItemQuantity = int.Parse(Console.ReadLine()!);
            info.ItemQuantity = userInputItemQuantity;
            return info;
        }

        private void RemoveInfoFromList()
        {
            Console.Clear();
            System.Console.WriteLine("Which Item would you like to Delete from Database?");

            List<Delivery> infoList = new List<Delivery>();

            if(infoList.Count > 0)
            {
                #region 
                int count = 0;

                foreach(Delivery info in infoList)
                {
                    count++;
                    System.Console.WriteLine($"{count}. {info.Id}");
                }
                #endregion

                int targetInfoId = int.Parse(Console.ReadLine()!);
                int targetIndex = targetInfoId - 1;

                if(targetIndex >= 0 && targetIndex < infoList.Count)
                {
                    Delivery desiredInfo = infoList[targetIndex];

                    if(_dRepo.DeleteExistingInfo(desiredInfo))
                        System.Console.WriteLine($"{desiredInfo.CustomerId} was Successfully Deleted.");
                    else
                        System.Console.WriteLine($"{desiredInfo.CustomerId} Failed to be Deleted.");
                }
                else
                {
                    System.Console.WriteLine("Invalid Id Selection.");
                }
            }
            else
            {
                System.Console.WriteLine("There is no available Info.");
            }
            PressAnyKey();
        }

        private bool CloseApplication()
        {
            System.Console.WriteLine("Application Closing:");
            PressAnyKey();
            Console.Clear();
            return false;
        }

        private void PressAnyKey()
        {
            System.Console.WriteLine("Press Any Key To Continue....");
            Console.ReadKey();
        }
    }
}