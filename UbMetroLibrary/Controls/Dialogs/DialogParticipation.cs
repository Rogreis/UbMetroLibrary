// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Windows;

namespace MahApps.Metro.Controls.Dialogs
{
    public static class DialogParticipation
    {
        private static readonly IDictionary<object, DependencyObject> ContextRegistrationIndex = new Dictionary<object, DependencyObject>();

        public static readonly DependencyProperty RegisterProperty
            = DependencyProperty.RegisterAttached("Register",
                                                  typeof(object),
                                                  typeof(DialogParticipation),
                                                  new PropertyMetadata(null, RegisterPropertyChangedCallback));

        private static void RegisterPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (dependencyPropertyChangedEventArgs.OldValue != null)
            {
                ContextRegistrationIndex.Remove(dependencyPropertyChangedEventArgs.OldValue);
            }

            if (dependencyPropertyChangedEventArgs.NewValue != null)
            {
                ContextRegistrationIndex[dependencyPropertyChangedEventArgs.NewValue] = dependencyObject;
            }
        }

        public static void SetRegister(DependencyObject element, object? context)
        {
            element.SetValue(RegisterProperty, context);
        }

        public static object? GetRegister(DependencyObject element)
        {
            return element.GetValue(RegisterProperty);
        }

        internal static bool IsRegistered(object context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return ContextRegistrationIndex.ContainsKey(context);
        }

        internal static DependencyObject GetAssociation(object context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return ContextRegistrationIndex[context];
        }
    }
}