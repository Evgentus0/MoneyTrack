using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.AppServices.Tests.Data
{
    internal partial class Data
    {
        internal static class TransactionData
        {
            public static IEnumerable<object[]> UpdateCases()
            {
                var startDate = DateTime.Now.AddMonths(-1);

                var acc = new Account
                {
                    Name = "acc1",
                    Id = 1,
                };

                var ctg = new Category
                {
                    Name = "ctg1",
                    Id = 1
                };

                yield return new object[]
                {
                    new List<Transaction>
                    {
                        new Transaction
                        {
                            Id = 1,
                            Quantity = 100,
                            Description = "tran1",
                            Account = acc,
                            Category = ctg,
                            AccountRest = 100,
                            AddedDttm = startDate
                        },
                        new Transaction
                        {
                            Id = 2,
                            Quantity = 200,
                            Description = "tran2",
                            Account = acc,
                            Category = ctg,
                            AccountRest = 300,
                            AddedDttm = startDate.AddDays(1)
                        },
                        new Transaction
                        {
                            Id = 3,
                            Quantity = 300,
                            Description = "tran3",
                            Account = acc,
                            Category = ctg,
                            AccountRest = 600,
                            AddedDttm = startDate.AddDays(2)
                        }
                    },
                    new TransactionDto
                    {
                        Id = 1,
                        Quantity = 150
                    },
                    new List<Transaction>
                    {
                        new Transaction
                        {
                            Id = 1,
                            Quantity = 150,
                            Description = "tran1",
                            Account = acc,
                            Category = ctg,
                            AccountRest = 150,
                            AddedDttm = startDate
                        },
                        new Transaction
                        {
                            Id = 2,
                            Quantity = 200,
                            Description = "tran2",
                            Account = acc,
                            Category = ctg,
                            AccountRest = 350,
                            AddedDttm = startDate.AddDays(1)
                        },
                        new Transaction
                        {
                            Id = 3,
                            Quantity = 300,
                            Description = "tran3",
                            Account = acc,
                            Category = ctg,
                            AccountRest = 650,
                            AddedDttm = startDate.AddDays(2)
                        }
                    }
                };

                yield return new object[]
                {
                    new List<Transaction>
                    {
                        new Transaction
                        {
                            Id = 1,
                            Quantity = 100,
                            Description = "tran1",
                            Account = acc,
                            Category = ctg,
                            AccountRest = 100,
                            AddedDttm = startDate
                        },
                        new Transaction
                        {
                            Id = 2,
                            Quantity = 200,
                            Description = "tran2",
                            Account = acc,
                            Category = ctg,
                            AccountRest = 300,
                            AddedDttm = startDate.AddDays(1)
                        },
                        new Transaction
                        {
                            Id = 3,
                            Quantity = 300,
                            Description = "tran3",
                            Account = acc,
                            Category = ctg,
                            AccountRest = 600,
                            AddedDttm = startDate.AddDays(2)
                        }
                    },
                    new TransactionDto
                    {
                        Id = 1,
                        AddedDttm = startDate.AddDays(1).AddHours(2)
                    },
                    new List<Transaction>
                    {
                        new Transaction
                        {
                            Id = 1,
                            Quantity = 100,
                            Description = "tran1",
                            Account = acc,
                            Category = ctg,
                            AccountRest = 300,
                            AddedDttm = startDate.AddDays(1).AddHours(2)
                        },
                        new Transaction
                        {
                            Id = 2,
                            Quantity = 200,
                            Description = "tran2",
                            Account = acc,
                            Category = ctg,
                            AccountRest = 200,
                            AddedDttm = startDate.AddDays(1)
                        },
                        new Transaction
                        {
                            Id = 3,
                            Quantity = 300,
                            Description = "tran3",
                            Account = acc,
                            Category = ctg,
                            AccountRest = 600,
                            AddedDttm = startDate.AddDays(2)
                        }
                    }
                };
            }
        }
    }
}
