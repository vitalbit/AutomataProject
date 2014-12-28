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
        public static DalUser ToDalUser(this User user)
        {
            return new DalUser()
            {
                Id = user.UserId,
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
                UserId = user.Id,
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
                Id = answer.AttachmentContentId,
                TestId = answer.TestId,
                UserId = answer.UserId,
                Mark = answer.Mark
            };
        }

        public static Answer ToOrmAnswer(this DalAnswer answer)
        {
            return new Answer()
            {
                AttachmentContentId = answer.Id,
                TestId = answer.TestId,
                UserId = answer.UserId,
                Mark = answer.Mark
            };
        }

        public static DalAttachmentContent ToDalAttachmentContent(this AttachmentContent content)
        {
            return new DalAttachmentContent()
            {
                Id = content.AttachmentContentId,
                FileName = content.FileName,
                Content = content.Content
            };
        }

        public static AttachmentContent ToOrmAttachmentContent(this DalAttachmentContent content)
        {
            return new AttachmentContent()
            {
                AttachmentContentId = content.Id,
                FileName = content.FileName,
                Content = content.Content
            };
        }

        public static DalBlock ToDalBlock(this Block block)
        {
            return new DalBlock()
            {
                Id = block.BlockId,
                Title = block.Title,
                Text = block.Text,
                BlockTypeId = block.BlockTypeId
            };
        }

        public static Block ToOrmBlock(this DalBlock block)
        {
            return new Block()
            {
                BlockId = block.Id,
                Title = block.Title,
                Text = block.Text,
                BlockTypeId = block.BlockTypeId
            };
        }

        public static DalBlockType ToDalBlockType(this BlockType blockType)
        {
            return new DalBlockType()
            {
                Id = blockType.BlockTypeId,
                Name = blockType.Name
            };
        }

        public static BlockType ToOrmBlockType(this DalBlockType blockType)
        {
            return new BlockType()
            {
                BlockTypeId = blockType.Id,
                Name = blockType.Name
            };
        }

        public static DalCourse ToDalCourse(this Course course)
        {
            return new DalCourse()
            {
                Id = course.CourseId,
                Number = course.Number
            };
        }

        public static Course ToOrmCourse(this DalCourse course)
        {
            return new Course()
            {
                CourseId = course.Id,
                Number = course.Number
            };
        }

        public static DalFaculty ToDalFaculty(this Faculty faculty)
        {
            return new DalFaculty()
            {
                Id = faculty.FacultyId,
                Name = faculty.Name
            };
        }

        public static Faculty ToOrmFaculty(this DalFaculty faculty)
        {
            return new Faculty()
            {
                FacultyId = faculty.Id,
                Name = faculty.Name
            };
        }

        public static DalGroup ToDalGroup(this Group group)
        {
            return new DalGroup()
            {
                Id = group.GroupId,
                Name = group.Name
            };
        }

        public static Group ToOrmGroup(this DalGroup group)
        {
            return new Group()
            {
                GroupId = group.Id,
                Name = group.Name
            };
        }

        public static DalRole ToDalRole(this Role role)
        {
            return new DalRole()
            {
                Id = role.RoleId,
                Name = role.Name
            };
        }

        public static Role ToOrmRole(this DalRole role)
        {
            return new Role()
            {
                RoleId = role.Id,
                Name = role.Name
            };
        }

        public static DalSpeciality ToDalSpeciality(this Speciality speciality)
        {
            return new DalSpeciality()
            {
                Id = speciality.SpecialityId,
                Name = speciality.Name
            };
        }

        public static Speciality ToOrmSpeciality(this DalSpeciality speciality)
        {
            return new Speciality()
            {
                SpecialityId = speciality.Id,
                Name = speciality.Name
            };
        }

        public static DalTest ToDalTest(this Test test)
        {
            return new DalTest()
            {
                Id = test.TestId,
                Name = test.Name,
                TestCount = test.TestCount,
                TestTime = test.TestTime
            };
        }

        public static Test ToOrmTest(this DalTest test)
        {
            return new Test()
            {
                TestId = test.Id,
                Name = test.Name,
                TestCount = test.TestCount,
                TestTime = test.TestTime
            };
        }
    }
}
