using DataSourceService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSourceService
{
    public class EmployeeGateWay: ITableDataGateway<EmployeeDB>
    {
        private WFlowContext db;

        public EmployeeGateWay(WFlowContext context)
        {
            this.db = context;
        }

        public IEnumerable<EmployeeDB> GetAll()
        {
            return db.Employees;
        }

        public EmployeeDB GetByID(int Id)
        {
            return db.Employees.Find(Id);
        }

        public IEnumerable<EmployeeDB> GetByCondition(Func<EmployeeDB, bool> predicate)
        {
            return db.Employees.Where(predicate);
        }

        public void Create(EmployeeDB employee)
        {
            db.Employees.Add(employee);
        }

        public void Update(EmployeeDB employee)
        {
            //db.Entry(employee).State = EntityState.Modified;
            db.Entry(db.Employees.Find(employee.Id)).CurrentValues.SetValues(employee);
        }

        public void Delete(int Id)
        {
            EmployeeDB employee = db.Employees.Find(Id);
            if (employee != null)
                db.Employees.Remove(employee);
        }
    }
}
