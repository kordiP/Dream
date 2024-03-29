﻿using Dream.Data.Models;
using Dream.Repositories;

namespace Dream.WPF.Controllers
{
    public class UserDepositController
    {

        private DreamContext context;

        private UserRepository userRepository;
        private UserView userView;
        public UserDepositController(DreamContext context)
        {
            this.context = context;

            userRepository = new UserRepository(context);
        }
        public UserDepositController(DreamContext context, UserView userView)
        {
            this.context = context;

            userRepository = new UserRepository(context);
            this.userView = userView;
        }
        public decimal Deposit(User user)
        {
            /* Validation */
            if (user.Balance is null && IsDepositValid(userView.DepositAmount))
            {
                user.Balance = 0;
                user.Balance += userView.DepositAmount;
            }
            else if (IsDepositValid(userView.DepositAmount))
            {
                user.Balance += userView.DepositAmount;
            }
            else
            {
                userView.InvalidDeposit();
                return 0;
            }

            /* Depositing money */
            userRepository.Update(user);
            userView.SuccessfulDeposit((decimal)user.Balance);

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
