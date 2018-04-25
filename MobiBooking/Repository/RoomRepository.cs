﻿using MobiBooking.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace MobiBooking.Models.Repository
{
    public class RoomRepository : IDefaultRepository<Room>
    {
        private readonly BookingDbContext _db;

        public RoomRepository(BookingDbContext db)
        {
            _db = db;
        }

        public int Add(Room b)
        {
            _db.Rooms.Add(b);
            _db.SaveChanges();
            return b.Id;
        }

        public int Delete(int id)
        {
            var room = _db.Rooms.FirstOrDefault(c => c.Id == id);
            if(room == null)
            {
                throw new HttpResponseException(404, "Can't find room with this identifier!");
            }
            _db.Rooms.Remove(room);
            _db.SaveChanges();
            
            return id;
        }

        public Room Get(int id)
        {
            var room = _db.Rooms.FirstOrDefault(c => c.Id == id);
            if(room == null)
            {
                throw new HttpResponseException(404, "Can't find room with this identifier!");
            }

            return room;
        }

        public IEnumerable<Room> GetAll()
        {
            return _db.Rooms.OrderBy(c => c.Name);
        }

        public int Update(int id, Room item)
        {
            var room = _db.Rooms.FirstOrDefault(c => c.Id == id);
            if (room == null)
            {
                throw new HttpResponseException(404, "Can't find room with this identifier!");
            }
            try
            {
                room.Name = item.Name;
                room.EtimeOption = item.EtimeOption;
                room.Localization = item.Localization;
                room.IsActive = item.IsActive;
                room.Capacity = item.Capacity;
                room.IsReserved = item.IsReserved;
                room.EbookStatus = item.EbookStatus;
                _db.SaveChanges();
            }
            catch
            {
                throw new HttpResponseException(400, "Invalid sended data!");
            }
            return room.Id;
        }
    }
}