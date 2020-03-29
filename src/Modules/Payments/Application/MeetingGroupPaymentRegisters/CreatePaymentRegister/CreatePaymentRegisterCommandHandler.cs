﻿using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingGroupPaymentRegisters;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingGroupPaymentRegisters.CreatePaymentRegister
{
    internal class CreatePaymentRegisterCommandHandler : ICommandHandler<CreatePaymentRegisterCommand, Guid>
    {
        private readonly IMeetingGroupPaymentRegisterRepository _meetingGroupPaymentRegisterRepository;

        public CreatePaymentRegisterCommandHandler(IMeetingGroupPaymentRegisterRepository meetingGroupPaymentRegisterRepository)
        {
            _meetingGroupPaymentRegisterRepository = meetingGroupPaymentRegisterRepository;
        }

        public async Task<Guid> Handle(CreatePaymentRegisterCommand command, CancellationToken cancellationToken)
        {
            var paymentRegister = MeetingGroupPaymentRegister.CreatePaymentScheduleForMeetingGroup(
                new MeetingGroupId(command.MeetingGroupProposalId));

            await _meetingGroupPaymentRegisterRepository.AddAsync(paymentRegister);

            return paymentRegister.Id.Value;
        }
    }
}