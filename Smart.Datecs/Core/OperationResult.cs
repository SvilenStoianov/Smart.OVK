using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Datecs
{
    /// <summary>
    /// Generic клас за резултат от действие (CheckResult)
    /// </summary>
    /// <typeparam name="T">Типа на резултата</typeparam>
    public class OperationResult<T>
    {
        /// <summary>
        /// Default празна инстанция
        /// </summary>
        public static readonly OperationResult Empty = new OperationResult();

        /// <summary>
        /// Конструктор
        /// </summary>
        public OperationResult()
        {
            this.Result = default(T);
            this.Errors = new List<string>();
            this.Warnings = new List<string>();
        }
        /// <summary>
        /// Конструктор с параметър за резултат
        /// </summary>
        /// <param name="result">Резултат</param>
        public OperationResult(T result)
        {
            this.Result = result;
            this.Errors = new List<string>();
            this.Warnings = new List<string>();
        }
        /// <summary>
        /// Конструктор с параметър Exception който се добавя като грешка
        /// </summary>
        /// <param name="exception"></param>
        public OperationResult(Exception exception)
        {
            this.Result = default(T);
            this.Errors = new List<string>();
            this.Warnings = new List<string>();

            if (exception != null)
                this.Errors.Add("Грешка: " + exception.ToString());
        }
        /// <summary>
        /// Конструктор с параметри за грешки
        /// </summary>
        /// <param name="errors">Грешки</param>
        public OperationResult(params string[] errors)
        {
            this.Result = default(T);
            this.Errors = new List<string>();
            this.Warnings = new List<string>();
            if (errors != null && errors.Length > 0)
                this.Errors.AddRange(errors);
        }

        /// <summary>
        /// Резултат от операцията
        /// </summary>
        public virtual T Result { get; set; }
        /// <summary>
        /// Tag за допълнителни данни
        /// </summary>
        public virtual object Tag { get; set; }
        /// <summary>
        /// Грешки
        /// </summary>
        public virtual List<string> Errors { get; set; }
        /// <summary>
        /// Предупреждения
        /// </summary>
        public virtual List<string> Warnings { get; set; }

        /// <summary>
        /// Проверка дали има грешки
        /// </summary>
        public virtual bool Success
        {
            get
            {
                return this.Errors.Count == 0;
            }
        }
        /// <summary>
        /// Проверка дали има предупреждения
        /// </summary>
        public virtual bool Warning
        {
            get
            {
                return this.Warnings.Count != 0;
            }
        }
        /// <summary>
        /// Конкатенирани грешки (ако има)
        /// </summary>
        public virtual string Message
        {
            get
            {
                return string.Join<string>(Environment.NewLine, this.Errors);
            }
        }

        /// <summary>
        /// Добавя нова грешка
        /// </summary>
        /// <param name="message">Съобщение</param>
        public virtual void AddError(string message)
        {
            this.Errors.Add(message);
        }
        /// <summary>
        /// Добавя ново предупреждение
        /// </summary>
        /// <param name="message">Съобщение</param>
        public virtual void AddWarnings(string message)
        {
            this.Warnings.Add(message);
        }
    }

    /// <summary>
    /// Клас за резултат от действие (CheckResult)
    /// </summary>
    public class OperationResult : OperationResult<object>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public OperationResult() : base() { }
        /// <summary>
        /// Конструктор с параметър за резултат
        /// </summary>
        /// <param name="result">Резултат</param>
        public OperationResult(object result) : base(result) { }
        /// <summary>
        /// Конструктор с параметър Exception който се добавя като грешка
        /// </summary>
        /// <param name="exception"></param>
        public OperationResult(Exception exception) : base(exception) { }
        /// <summary>
        /// Конструктор с параметри за грешки
        /// </summary>
        /// <param name="errors"></param>
        public OperationResult(params string[] errors) : base(errors) { }
    }
}
