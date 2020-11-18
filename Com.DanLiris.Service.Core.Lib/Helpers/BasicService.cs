using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib.Service;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Com.DanLiris.Service.Core.Lib.Helpers
{
    public abstract class BasicService<TDbContext, TModel> : StandardEntityService<TDbContext, TModel>
        where TDbContext : DbContext
        where TModel : StandardEntity, IValidatableObject
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public BasicService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public virtual async Task<int> CreateModel(TModel Model)
        {
            var result = await this.CreateAsync(Model);
            SetCache();
            return result;
        }

        protected virtual void SetCache()
        {

        }

        public abstract Tuple<List<TModel>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}");

        public virtual async Task<TModel> ReadModelById(int Id)
        {
            var result = await this.GetAsync(Id);
            SetCache();
            return result;
        }

        public virtual async Task<int> UpdateModel(int Id, TModel Model)
        {
            var result = await this.UpdateAsync(Id, Model);
            SetCache();
            return result;
        }

        public override void OnUpdating(int id, TModel model)
        {
            base.OnUpdating(id, model);
            model._LastModifiedAgent = "core-service";
            model._LastModifiedBy = this.Username ?? "";
        }

        public override void OnCreating(TModel model)
        {
            base.OnCreating(model);
            model._CreatedAgent = "core-service";
            model._CreatedBy = this.Username ?? "";
            model._LastModifiedAgent = "core-service";
            model._LastModifiedBy = this.Username ?? "";
        }

        public override void OnDeleting(TModel model)
        {
            base.OnDeleting(model);
            model._DeletedAgent = "core-service";
            model._DeletedBy = this.Username ?? "";
        }

        public virtual async Task<int> DeleteModel(int Id)
        {
            var result = await this.DeleteAsync(Id);
            SetCache();
            return result;
        }

        public virtual IQueryable<TModel> ConfigureFilter(IQueryable<TModel> Query, Dictionary<string, object> FilterDictionary)
        {
            if (FilterDictionary != null && !FilterDictionary.Count.Equals(0))
            {
                foreach (var f in FilterDictionary)
                {
                    string Key = f.Key;
                    object Value = f.Value;
                    string filterQuery = string.Concat(string.Empty, Key, " == @0");

                    Query = Query.Where(filterQuery, Value);
                }
            }
            return Query;
        }

    }
}