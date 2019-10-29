using DataLayer.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
   public class ReportRepository
    {
        private DataContext context;

        public ReportRepository() { this.context = new DataContext(); }

        public int Insert(Report report)
        {
            this.context.Reports.Add(report);
            return this.context.SaveChanges();
        }

        public List<Report> GetReportByUserId(int id)
        {
            return this.context.Reports.Where(u => u.ReportedBy == id).ToList();

        }

        public Report Get(int id)
        {
            return this.context.Reports.SingleOrDefault(u => u.ReportId == id);

        }

        public List<Report> GetAll(string reportType)
        {
            return this.context.Reports.Include("ReportedByUser").Include("ReportedIdUser").Include("Restaurant").Include("Post").Include("Recipe").Where(r=>r.Type== reportType).ToList();
        }

        public List<Report> GetAllReports()
        {
            return this.context.Reports.ToList();
        }

        public List<Report> GetAllByTypeName(string name)
        {
            return this.context.Reports.ToList();
        }
    }
}
