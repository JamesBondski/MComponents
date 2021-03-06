﻿using MComponents.MForm;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MComponents
{
    public class MFormContainerContext
    {
        public event EventHandler<MFormContainerContextSubmitArgs> OnFormSubmit;

        protected object mLocker = new object();

        public List<IMForm> Forms { get; set; } = new List<IMForm>();

        public void RegisterForm(IMForm pForm)
        {
            Forms.Add(pForm);
        }

        public bool NotifySubmit(IStringLocalizer<MComponentsLocalization> pLocalizer)
        {
            bool submitSuccessful = true;

            lock (mLocker)
            {
                if (OnFormSubmit == null)
                    return submitSuccessful;

                var args = new MFormContainerContextSubmitArgs()
                {
                    UserInterated = true
                };

                foreach (var handler in OnFormSubmit.GetInvocationList())
                {
                    try
                    {
                        handler.Method.Invoke(handler.Target, new object[] { this, args });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error in the handler {0}: {1}", handler.Method.Name, e.Message);

                        string msg = e.ToString();

                        if (e is TargetInvocationException te)
                        {
                            msg = te.InnerException.ToString();

                            if (te.InnerException is UserMessageException ue)
                                msg = ue.Message;
                        }

                        Notificator.InvokeNotification(true, msg);
                        submitSuccessful = false;
                    }
                }

                Notificator.InvokeNotification(false, pLocalizer["Gespeichert!"]);
            }

            return submitSuccessful;
        }
    }
}
