using AutoMapper;
using moneytrackercore.data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moneytrackercore.Models
{
    public class MoneyTrackerCoreMappingProfile : Profile
    {
        public MoneyTrackerCoreMappingProfile()
        {
            CreateMap<Users, UsersModel>();
            CreateMap<Incomes, IncomesModel>();
            CreateMap<IncomeType, IncomeTypeModel>();
            CreateMap<Expenditure, ExpenditureModel>();
            CreateMap<ExpenditureConfig, ExpenditureConfigModel>();
            CreateMap<Balance, BalanceModel>();
            CreateMap<PaymentMode, PaymentModeModel>();
        }
    }
}
