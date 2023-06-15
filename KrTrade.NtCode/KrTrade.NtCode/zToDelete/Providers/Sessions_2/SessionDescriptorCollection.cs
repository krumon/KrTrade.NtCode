//namespace KrTrade.NtCode
//{
//    /// <summary>
//    /// Represents a deafault implementation of session descriptor collection.
//    /// </summary>
//    public class SessionDescriptorCollection : ServiceCollection<SessionDescriptor>
//    {

//        #region Implementation methods

//        /// <inheritdoc/>
//        public override void Add(SessionDescriptor item)
//        {
//            if (_descriptors.Count < 1)
//            {
//                _descriptors.Add(item);
//                return;
//            }

//            int count = _descriptors.Count;
//            for (int i = 0; i < count; i++)
//            {
//                if (item.Duration > _descriptors[i].Duration)
//                {
//                    _descriptors.Insert(i, item);
//                    break;
//                }
//                if (i == Count - 1)
//                {
//                    _descriptors.Add(item);
//                    break;
//                }
//            }
//        }

//        /// <inheritdoc/>
//        public override bool Remove(SessionDescriptor item)
//        {
//            return _descriptors.Remove(item);
//        }

//        #endregion

//    }
//}
