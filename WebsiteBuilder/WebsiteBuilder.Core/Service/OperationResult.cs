using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteBuilder.Core.Service
{
    public class OperationResult
    {
        public OperationResult()
        {
            Errors = new List<string>();
        }

        public OperationResult(string errorMessage)
            : this()
        {
            Errors.Add(errorMessage);
        }

        public OperationResult(List<string> errorMessageList)
        {
            foreach (var error in errorMessageList)
            {
                Errors.Add(error);
            }
        }

        public OperationResult(OperationResult other)
            : this()
        {
            this.MergeWith(other);
        }

        public List<string> Errors { get; set; }

        public bool Success
        {
            get { return Errors != null && !Errors.Any(); }
        }

        public OperationResult MergeWith(OperationResult other)
        {
            foreach (var err in other.Errors)
            {
                this.Errors.Add(err);
            }

            return this;
        }

        public string FormatErrors()
        {
            return FormatErrors(Environment.NewLine);
        }

        public string FormatErrors(string separator)
        {
            return string.Join(separator, Errors);
        }


    }

    public class OperationResult<T> : OperationResult
    {
        public T Result { get; set; }

        public OperationResult()
            : base()
        {

        }

        public OperationResult(OperationResult other)
            : base(other)
        {

        }

        public OperationResult(T result)
            : base()
        {
            if (typeof(T) == typeof(string))
            {
                throw new ApplicationException("Cannot determine which constructor to use, because T = string");
            }
            this.Result = result;
        }

        public OperationResult(string errorMessage)
            : base(errorMessage)
        {
            if (typeof(T) == typeof(string))
            {
                throw new ApplicationException("Cannot determine which constructor to use, because T = string");
            }
        }
    }
}
