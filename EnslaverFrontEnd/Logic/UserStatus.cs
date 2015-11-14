using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnslaverCore;
using EnslaverCore.Logic;

namespace EnslaverFrontEnd.Logic
{
    class User : IEquatable<User>
    {
        public string name;
        public int headFound = 0;
        public int eyesFound = 0;
        public int smiling = 0;
        public bool isBlinked = false;


        public User(string name)
        {
            this.name = name;
        }

        bool IEquatable<User>.Equals(User other)
        {
            return other.name == this.name;
        }
    }

    class UserStatus
    {
        private List<User> users = new List<User>()
        {
            new User(AppGlobalContext.GetInstance().OwnerId)
        };

        int frameCounter;
        DateTime last = DateTime.Now;
        const int accuracyMargin = 50;

        public void CheckFrame(List<HeadInformation> heads)
        {
            frameCounter++;

            if (heads == null) return;

            foreach (HeadInformation h in heads)
            {
                foreach (User u in users)
                {
                    if (u.name == h.Name)
                    {
                        u.headFound++;
                        if (h.IsSmile) u.smiling++;
                        if (((h.Eye1.Height != 0 && h.Eye1.Width != 0)  || (h.Eye2.Height != 0 && h.Eye2.Width != 0))) u.eyesFound++;
                        else u.isBlinked = true;
                    }

                }
            }
        }

        public void ResetCounters()
        {
            foreach (User u in users)
            {
                u.smiling = 0;
                u.headFound = 0;
                u.eyesFound = 0;
            }
            frameCounter = 0;
            last = DateTime.Now;
        }

        public void BlinkReset()
        {
            foreach (User u in users) u.isBlinked = false;
        }

        public bool isHuman(User u)
        {
            return u.isBlinked;
        }

        public UserStates GetUserStatus(string owner)
        {
            if (users != null)
            {
                User ownerUser = users.Find(c => c.name == owner);
                if (ownerUser != null)
                {
                    return GetUserStatus(ownerUser);
                }
            }
            return UserStates.HeadNotFound;
        }

        void UpdateCoordinates(HeadInformation head, User user)
        {
            int headY = head.Head.Top;
            int headX = head.Head.Left;
        }

        private UserStates previousState;

        public UserStates GetUserStatus(User u)
        {
            if (frameCounter != 0)
            {
                if (((double)u.headFound / frameCounter) * 100 > accuracyMargin)
                {
                    if (((double)u.eyesFound / frameCounter) * 100 > accuracyMargin)
                    {
                        if (((double)u.smiling / frameCounter) * 100 > accuracyMargin)
                        {
                            this.previousState = UserStates.Smiling;
                        }
                        else
                        {
                            this.previousState = UserStates.Fine;
                        }
                    }
                    else this.previousState = UserStates.EyesNotFound;
                }
                else this.previousState = UserStates.HeadNotFound;
            }

            return this.previousState;
        }
    }
}
