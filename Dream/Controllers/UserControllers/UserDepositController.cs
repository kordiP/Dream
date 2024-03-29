﻿using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views.UserViews;

namespace Dream.Controllers.UserControllers
{
                /* --- Summary --- */
    /* --- This controller is responsible for --- */
    /* --- operations related to user balance --- */

    public class UserDepositController
    {
        private UserRepository userRepository;
        private UserDepositView depositView;

        private DreamContext context;

        public UserDepositController(DreamContext context)
        {
            this.context = context;

            this.userRepository = new UserRepository(context);
        }
        public decimal Deposit(User user)
        {
            /* --- Getting values --- */
            depositView = new UserDepositView(user.Username);

            /* --- Validation --- */
            if(user.Balance is null && IsDepositValid(depositView.Amount))
            {
                user.Balance = 0;
                user.Balance += depositView.Amount;
            }
            else if (IsDepositValid(depositView.Amount))
            {
                user.Balance += depositView.Amount;
            }
            else
            {
                depositView.InvalidDeposit();
                return 0;
            }

            /* --- Saving changes --- */
            userRepository.Update(user);
            depositView.SuccessfulDeposit((decimal)user.Balance);

            return (decimal)user.Balance;
        }
        public bool IsDepositValid(decimal deposit)
        {
            if (deposit >= 0) return true;
            return false;
        }
        public int Purchase(decimal gamePrice, User user)
        {
            user.Balance -= gamePrice;

            userRepository.Update(user);

            return user.UserId;
        }
    }
}
