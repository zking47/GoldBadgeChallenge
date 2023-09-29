using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoldBadgeChallenge.Data.Entities;
using GoldBadgeChallenge.Data.Entities.Enums;

namespace GoldBadgeChallenge.Repository
{
    public class DeliveryRepository
    {
        protected readonly List<Delivery> _infoDb = new List<Delivery>();
        private int _count;

        public DeliveryRepository()
        {
            Seed();
        }

        private void Seed()
        {
            Delivery deliveryA = new Delivery(new DateTime(2023,09,12), new DateTime(2023,09,17), OrderStatus.EnRoute, 27, 3, 1);
            Delivery deliveryB = new Delivery(new DateTime(2023,09,18), new DateTime(2023,09,24), OrderStatus.EnRoute, 27, 3, 1);
            Delivery deliveryC = new Delivery(new DateTime(2023,09,27), new DateTime(2023,10,12), OrderStatus.Delivered, 27, 3, 1);

            InfoToDb(deliveryA);
            InfoToDb(deliveryB);
            InfoToDb(deliveryC);
        }

        public bool InfoToDb(Delivery info)
        {
            if (info is null)
            {
                return false;
            }
            else
            {
                _count++;
                info.Id=_count;
                _infoDb.Add(info);
                return true;
            }
        }
        public List<Delivery> GetAllInfo()
        {
            return _infoDb;
        }
        public Delivery GetDeliveryById(int id)
        {
            foreach(Delivery info in _infoDb)
            {
                if (info.Id == id)
                {
                    return info;
                }
            }
            return null;
        }
        public bool UpdateExistingDelivery(int originalId, Delivery updatedData)
        {
            Delivery infoInDb = GetDeliveryById(originalId);
            if (infoInDb != null)
            {
                infoInDb.Id = updatedData.Id;
                infoInDb.OrderStatus = updatedData.OrderStatus;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteExistingInfo(Delivery info)
        {
            bool deleteResult = _infoDb.Remove(info);
            return deleteResult;
        }
        public List<Delivery> GetInfoByOrderStatus(OrderStatus orderStatus)
        {
            List<Delivery> aux = new List<Delivery>();
            foreach (Delivery item in _infoDb)
            {
                if (item.OrderStatus == orderStatus)
                {
                    aux.Add(item);
                }
            }
            return aux;
        }

    }
}