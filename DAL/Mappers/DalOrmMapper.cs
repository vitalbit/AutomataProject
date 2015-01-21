using DAL.Interface.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class DalOrmMapper
    {
        #region Mappers
        public static IDalEntity ToDal(this IORMEntity entity)
        {
            if (entity is Answer)
                return (entity as Answer).ToDalAnswer();
            else if (entity is AttachmentContent)
                return (entity as AttachmentContent).ToDalAttachmentContent();
            else if (entity is Block)
                return (entity as Block).ToDalBlock();
            else if (entity is BlockType)
                return (entity as BlockType).ToDalBlockType();
            else if (entity is Course)
                return (entity as Course).ToDalCourse();
            else if (entity is Faculty)
                return (entity as Faculty).ToDalFaculty();
            else if (entity is Group)
                return (entity as Group).ToDalGroup();
            else if (entity is Role)
                return (entity as Role).ToDalRole();
            else if (entity is Speciality)
                return (entity as Speciality).ToDalSpeciality();
            else if (entity is Test)
                return (entity as Test).ToDalTest();
            else if (entity is User)
                return (entity as User).ToDalUser();
            else
                return null;
        }

        public static IORMEntity ToOrm(this IDalEntity entity)
        {
            if (entity is DalAnswer)
                return (entity as DalAnswer).ToOrmAnswer();
            else if (entity is DalAttachmentContent)
                return (entity as DalAttachmentContent).ToOrmAttachmentContent();
            else if (entity is DalBlock)
                return (entity as DalBlock).ToOrmBlock();
            else if (entity is DalBlockType)
                return (entity as DalBlockType).ToOrmBlockType();
            else if (entity is DalCourse)
                return (entity as DalCourse).ToOrmCourse();
            else if (entity is DalFaculty)
                return (entity as DalFaculty).ToOrmFaculty();
            else if (entity is DalGroup)
                return (entity as DalGroup).ToOrmGroup();
            else if (entity is DalRole)
                return (entity as DalRole).ToOrmRole();
            else if (entity is DalSpeciality)
                return (entity as DalSpeciality).ToOrmSpeciality();
            else if (entity is DalTest)
                return (entity as DalTest).ToOrmTest();
            else if (entity is DalUser)
                return (entity as DalUser).ToOrmUser();
            else
                return null;
        }

        public static DalUser ToDalUser(this User user)
        {
            return new DalUser()
            {
                Id = user.Id,
                Nickname = user.Nickname,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Email = user.Email,
                CourseId = user.CourseId,
                GroupId = user.GroupId,
                SpecialityId = user.SpecialityId,
                FacultyId = user.FacultyId,
                RoleId = user.RoleId
            };
        }

        public static User ToOrmUser(this DalUser user)
        {
            return new User()
            {
                Id = user.Id,
                Nickname = user.Nickname,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Email = user.Email,
                CourseId = user.CourseId,
                GroupId = user.GroupId,
                SpecialityId = user.SpecialityId,
                FacultyId = user.FacultyId,
                RoleId = user.RoleId
            };
        }

        public static DalAnswer ToDalAnswer(this Answer answer)
        {
            return new DalAnswer()
            {
                Id = answer.Id,
                TestId = answer.TestId,
                UserId = answer.UserId,
                Mark = answer.Mark
            };
        }

        public static Answer ToOrmAnswer(this DalAnswer answer)
        {
            return new Answer()
            {
                Id = answer.Id,
                TestId = answer.TestId,
                UserId = answer.UserId,
                Mark = answer.Mark
            };
        }

        public static DalAttachmentContent ToDalAttachmentContent(this AttachmentContent content)
        {
            return new DalAttachmentContent()
            {
                Id = content.Id,
                FileName = content.FileName,
                Content = content.Content,
                Answer = content.Answer != null ? content.Answer.ToDalAnswer() : null,
                Blocks = content.Blocks != null ? content.Blocks.Select(ent => ent.ToDalBlock()) : null,
                Tests = content.Tests != null ? content.Tests.Select(ent => ent.ToDalTest()) : null
            };
        }

        public static AttachmentContent ToOrmAttachmentContent(this DalAttachmentContent content)
        {
            return new AttachmentContent()
            {
                Id = content.Id,
                FileName = content.FileName,
                Content = content.Content,
                Answer = content.Answer != null ? content.Answer.ToOrmAnswer() : null,
                Blocks = content.Blocks != null ? content.Blocks.Select(ent => ent.ToOrmBlock()).ToList() : null,
                Tests = content.Tests != null ? content.Tests.Select(ent => ent.ToOrmTest()).ToList() : null
            };
        }

        public static DalBlock ToDalBlock(this Block block)
        {
            return new DalBlock()
            {
                Id = block.Id,
                Title = block.Title,
                Text = block.Text,
                BlockTypeId = block.BlockTypeId
            };
        }

        public static Block ToOrmBlock(this DalBlock block)
        {
            return new Block()
            {
                Id = block.Id,
                Title = block.Title,
                Text = block.Text,
                BlockTypeId = block.BlockTypeId
            };
        }

        public static DalBlockType ToDalBlockType(this BlockType blockType)
        {
            return new DalBlockType()
            {
                Id = blockType.Id,
                Name = blockType.Name
            };
        }

        public static BlockType ToOrmBlockType(this DalBlockType blockType)
        {
            return new BlockType()
            {
                Id = blockType.Id,
                Name = blockType.Name
            };
        }

        public static DalCourse ToDalCourse(this Course course)
        {
            return new DalCourse()
            {
                Id = course.Id,
                Number = course.Number
            };
        }

        public static Course ToOrmCourse(this DalCourse course)
        {
            return new Course()
            {
                Id = course.Id,
                Number = course.Number
            };
        }

        public static DalFaculty ToDalFaculty(this Faculty faculty)
        {
            return new DalFaculty()
            {
                Id = faculty.Id,
                Name = faculty.Name
            };
        }

        public static Faculty ToOrmFaculty(this DalFaculty faculty)
        {
            return new Faculty()
            {
                Id = faculty.Id,
                Name = faculty.Name
            };
        }

        public static DalGroup ToDalGroup(this Group group)
        {
            return new DalGroup()
            {
                Id = group.Id,
                Name = group.Name
            };
        }

        public static Group ToOrmGroup(this DalGroup group)
        {
            return new Group()
            {
                Id = group.Id,
                Name = group.Name
            };
        }

        public static DalRole ToDalRole(this Role role)
        {
            return new DalRole()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static Role ToOrmRole(this DalRole role)
        {
            return new Role()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static DalSpeciality ToDalSpeciality(this Speciality speciality)
        {
            return new DalSpeciality()
            {
                Id = speciality.Id,
                Name = speciality.Name
            };
        }

        public static Speciality ToOrmSpeciality(this DalSpeciality speciality)
        {
            return new Speciality()
            {
                Id = speciality.Id,
                Name = speciality.Name
            };
        }

        public static DalTest ToDalTest(this Test test)
        {
            return new DalTest()
            {
                Id = test.Id,
                Name = test.Name,
                TestCount = test.TestCount,
                TestTime = test.TestTime,
                Answers = test.Answers != null ? test.Answers.Select(ent => ent.ToDalAnswer()).ToList() : null,
                AttachmentContents = test.AttachmentContents != null ? test.AttachmentContents.Select(ent => ent.ToDalAttachmentContent()).ToList() : null
            };
        }

        public static Test ToOrmTest(this DalTest test)
        {
            return new Test()
            {
                Id = test.Id,
                Name = test.Name,
                TestCount = test.TestCount,
                TestTime = test.TestTime,
                Answers = test.Answers != null ? test.Answers.Select(ent => ent.ToOrmAnswer()).ToList() : null,
                AttachmentContents = test.AttachmentContents != null ? test.AttachmentContents.Select(ent => ent.ToOrmAttachmentContent()).ToList() : null
            };
        }
        #endregion
        #region Copy

        public static void CopyToOrm(this IDalEntity dal, IORMEntity orm)
        {
            if (dal is DalUser && orm is User)
                (dal as DalUser).CopyToOrmUser((User)orm);
            else if (dal is DalTest && orm is Test)
                (dal as DalTest).CopyToOrmTest((Test)orm);
        }

        public static void CopyToOrmUser(this DalUser dalUser, User ormUser)
        {
            ormUser.CourseId = dalUser.CourseId;
            ormUser.Email = dalUser.Email;
            ormUser.FacultyId = dalUser.FacultyId;
            ormUser.FirstName = dalUser.FirstName;
            ormUser.GroupId = dalUser.GroupId;
            ormUser.LastName = dalUser.LastName;
            ormUser.Nickname = dalUser.Nickname;
            ormUser.Password = dalUser.Password;
            ormUser.RoleId = dalUser.RoleId;
            ormUser.SpecialityId = dalUser.SpecialityId;
        }

        public static void CopyToOrmTest(this DalTest dalTest, Test ormTest)
        {
            ormTest.Answers = dalTest.Answers.Select(ent => ent.ToOrmAnswer()).ToList();
            ormTest.AttachmentContents = dalTest.AttachmentContents.Select(ent => ent.ToOrmAttachmentContent()).ToList();
            ormTest.Name = dalTest.Name;
            ormTest.TestCount = dalTest.TestCount;
            ormTest.TestTime = dalTest.TestTime;
        }
        #endregion
    }
}
