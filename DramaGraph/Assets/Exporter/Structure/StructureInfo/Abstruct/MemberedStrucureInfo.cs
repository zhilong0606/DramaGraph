using System;
using System.Collections.Generic;

namespace Tool.Export.Structure
{
    public abstract class MemberedStrucureInfo<T> : MemberedStrucureInfo
        where T : BaseMemberInfo
    {
        protected List<T> m_memberList = new List<T>();

        public List<T> memberList
        {
            get { return m_memberList; }
        }
        
        protected MemberedStrucureInfo(string name) : base(name)
        {
        }

        public virtual bool CanAddMember(T memberInfo)
        {
            return true;
        }

        public T GetMember(string memberName)
        {
            for (int i = 0; i < m_memberList.Count; ++i)
            {
                if (m_memberList[i].name == memberName)
                {
                    return m_memberList[i];
                }
            }
            return null;
        }

        public void AddMember(T memberInfo)
        {
            m_memberList.Add(memberInfo);
        }

        public override BaseMemberInfo GetBaseMember(string memberName)
        {
            return GetMember(memberName);
        }

        public override void AddBaseMember(BaseMemberInfo memberInfo)
        {
            T info = memberInfo as T;
            if (info != null && CanAddMember(info))
            {
                AddMember(info);
            }
            else
            {
                throw new Exception(string.Format(StaticString.AddWrongTypeMemberFormat, name, memberInfo.name));
            }
        }
    }

    public abstract class MemberedStrucureInfo : BaseStructureInfo
    {
        public sealed override bool isCollection { get { return false; } }

        protected MemberedStrucureInfo(string name) : base(name)
        {
        }

        public bool HasMember(string memberName)
        {
            return GetBaseMember(memberName) != null;
        }

        public abstract BaseMemberInfo GetBaseMember(string memberName);
        public abstract void AddBaseMember(BaseMemberInfo memberInfo);
    }
}
