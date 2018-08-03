using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdentityDBfirstPOC.Models;
using IdentityDBfirstPOC.Models.ViewModels;
using IdentityDBfirstPOC.Repository;

namespace IdentityDBfirstPOC.Service
{
    public class AccountbookService
    {
        private readonly IRepository<AccountBook> _accountbookRepository;

        public AccountbookService(IUnitOfWork unitOfWork)
        {
            _accountbookRepository = new Repository<AccountBook>(unitOfWork);
        }

        public IEnumerable<SpendingTrackerViewModel> Lookup()
        {
            var source = _accountbookRepository.LookupAll();
            var result = source.Select(acctbook => new SpendingTrackerViewModel()
            {
                ID = acctbook.Id,
                TYPE = acctbook.Categoryyy,
                AMOUMT = acctbook.Amounttt,
                DATE = acctbook.Dateee,
                REMARK = acctbook.Remarkkk,
            });
            return result;
        }

        public SpendingTrackerViewModel GetSingle(Guid acctId)
        {
            var source = _accountbookRepository.GetSingle(d => d.Id == acctId);
            return new SpendingTrackerViewModel
            {
                ID = source.Id,
                TYPE = source.Categoryyy,
                AMOUMT = source.Amounttt,
                DATE = source.Dateee,
                REMARK = source.Remarkkk,
            };
        }

        public void Edit(Guid id, SpendingTrackerViewModel pageData)
        {
            var oldData = _accountbookRepository.GetSingle(d => d.Id == id);
            if (oldData != null)
            {
                oldData.Categoryyy = pageData.TYPE;
                oldData.Amounttt = pageData.AMOUMT;
                oldData.Dateee = pageData.DATE;
                oldData.Remarkkk = pageData.REMARK;
            }
        }

        public void Add(SpendingTrackerViewModel accountbook)
        {
            _accountbookRepository.Create(new AccountBook
            {
                Id = accountbook.ID,
                Categoryyy = accountbook.TYPE,
                Amounttt = accountbook.AMOUMT,
                Dateee = accountbook.DATE,
                Remarkkk = accountbook.REMARK,
            });

        }

        public void Delete(Guid id)
        {
            _accountbookRepository.Remove(_accountbookRepository.GetSingle(d => d.Id == id));
        }


    }
}