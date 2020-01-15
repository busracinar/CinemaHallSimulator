using CinemaHallSimulation.Entity;
using CinemaHallSimulation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaHallSimulation.Helpers
{
    class HelperTicket
    {
        public static (Ticket, bool) TicketCUD(Ticket ticket, EntityState entityState)
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                c.Entry(ticket).State = entityState;
                if (c.SaveChanges() > 0)
                {
                    return (ticket, true);
                }
                else
                {
                    return (ticket, false);
                }
            }
        }
        public static List<Ticket> GetTicketList()
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                return c.Ticket.ToList();
            }
        }
        public static List<TicketModel> GetTicketModelList()
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                List<TicketModel> ticketModels = new List<TicketModel>();
                List<Ticket> tickets = GetTicketList();
                foreach (var item in tickets)
                {
                    TicketModel ticketModel = new TicketModel();
                    ticketModel.TicketId = item.TicketId;
                    ticketModel.Showtimes = HelperShowtimes.GetShowtimesById(item.ShowtimesId);
                    ticketModel.Chair = HelperChair.GetChairById(item.ChairId);
                    if (item.Type == 0)
                    {
                        ticketModel.Type = "Tam";
                    }
                    else if (item.Type == 1)
                    {
                        ticketModel.Type = "İndirimli";
                    }
                    ticketModel.User = HelperUser.GetUserById(item.UserId);
                }
                return ticketModels;
            }
        }
        public static Ticket GetTicketByShowtimesIdAndChairId(int showtimesId, int chairId)
        {
            using (CinemaDbEntities c=new CinemaDbEntities())
            {
                return c.Ticket.Where(x => x.ShowtimesId == showtimesId && x.ChairId == chairId).FirstOrDefault();
            }
        }
    }
}
