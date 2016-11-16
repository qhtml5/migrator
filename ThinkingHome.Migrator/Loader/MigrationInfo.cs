﻿using System;
using System.Reflection;
using ThinkingHome.Migrator.Framework;
using ThinkingHome.Migrator.Framework.Interfaces;

namespace ThinkingHome.Migrator.Loader
{
    /// <summary>
    /// Информация о миграции. Обладает следующими свойствами:
    /// - обязательно  содержит класс миграции (!= null)
    /// - обязательно  содержит версию, соответствующую данному классу
    /// - обязательно  содержит значение свойства Ignore для данного класса
    /// - обязательно  содержит значение свойства WithoutTransaction для данного класса
    /// </summary>
    public struct MigrationInfo
    {
        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="type">Тип, из которого извлекается информация о миграции</param>
        public MigrationInfo(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!typeof(IMigration).IsAssignableFrom(type))
            {
                throw new InvalidCastException("Migration class must implement the IMigration interface");
            }

            var attribute = type.GetTypeInfo().GetCustomAttribute<MigrationAttribute>();
            if (attribute == null) throw new NullReferenceException("Migration attribute is not found");

            Type = type;
            Version = attribute.Version;
            Ignore = attribute.Ignore;
            WithoutTransaction = attribute.WithoutTransaction;
        }

        /// <summary>
        /// Тип миграции
        /// </summary>
        public readonly Type Type;

        /// <summary>
        /// Версия
        /// </summary>
        public readonly long Version;

        /// <summary>
        /// Признак: пропустить миграцию при выполнении
        /// </summary>
        public readonly bool Ignore;

        /// <summary>
        /// Признак: выполнить миграцию без транзакции
        /// </summary>
        public readonly bool WithoutTransaction;
    }
}