using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace pwp21rpa.Models
{
    public class BaseContext : DbContext
    {
        public ReadOnlyCollection<string> SoftDeleteTables = new ReadOnlyCollection<string>(new List<string>());
        public BaseContext(DbContextOptions options) : base(options)
        {

        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            BeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            BeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void BeforeSaving()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                var state = entry.State.ToString();
                if(state == "Added")
                {
                    try
                    {
                        entry.CurrentValues["CreatedAt"] = DateTime.Now;
                    }
                    catch
                    {}

                }
                else if(state == "Modified")
                {
                    try
                    {
                      entry.CurrentValues["UpdatedAt"] = DateTime.Now;
                    }
                    catch
                    {}
                }
                else if(state == "Deleted")
                {
                    try
                    {

                    }
                    catch (System.Exception)
                    {

                    }
                }
                if (state != "Added")
                {
                    try
                    {
                        entry.Property("CreatedAt").IsModified = false;
                        entry.Property("CreatedBy").IsModified = false;
                    }
                    catch (Exception)
                    {
                    }
                }
                if (this.SoftDeleteTables.Contains(entry.Entity.GetType().Name))
                {
                    try
                    {
                        if (state == "Deleted")
                        {
                            entry.State = EntityState.Modified;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

    }
}