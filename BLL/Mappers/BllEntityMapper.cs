using BLL.Interface.Entities;
using DAL.Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class BllEntityMapper
    {
        public static DalUser ToDalUser(this UserEntity user)
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

        public static UserEntity ToBllUser(this DalUser user)
        {
            return new UserEntity()
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

        public static DalAnswer ToDalAnswer(this AnswerEntity answer)
        {
            return new DalAnswer()
            {
                Id = answer.Id,
                TestId = answer.TestId,
                UserId = answer.UserId,
                Mark = answer.Mark
            };
        }

        public static AnswerEntity ToBllAnswer(this DalAnswer answer)
        {
            return new AnswerEntity()
            {
                Id = answer.Id,
                TestId = answer.TestId,
                UserId = answer.UserId,
                Mark = answer.Mark
            };
        }

        public static DalAttachmentContent ToDalAttachmentContent(this AttachmentContentEntity content)
        {
            return new DalAttachmentContent()
            {
                Id = content.Id,
                FileName = content.FileName,
                Content = content.Content
            };
        }

        public static AttachmentContentEntity ToBllAttachmentContent(this DalAttachmentContent content)
        {
            return new AttachmentContentEntity()
            {
                Id = content.Id,
                FileName = content.FileName,
                Content = content.Content
            };
        }

        public static DalBlock ToDalBlock(this BlockEntity block)
        {
            return new DalBlock()
            {
                Id = block.Id,
                Title = block.Title,
                Text = block.Text,
                BlockTypeId = block.BlockTypeId
            };
        }

        public static BlockEntity ToBllBlock(this DalBlock block)
        {
            return new BlockEntity()
            {
                Id = block.Id,
                Title = block.Title,
                Text = block.Text,
                BlockTypeId = block.BlockTypeId
            };
        }

        public static DalBlockType ToDalBlockType(this BlockTypeEntity blockType)
        {
            return new DalBlockType()
            {
                Id = blockType.Id,
                Name = blockType.Name
            };
        }

        public static BlockTypeEntity ToBllBlockType(this DalBlockType blockType)
        {
            return new BlockTypeEntity()
            {
                Id = blockType.Id,
                Name = blockType.Name
            };
        }

        public static DalCourse ToDalCourse(this CourseEntity course)
        {
            return new DalCourse()
            {
                Id = course.Id,
                Number = course.Number
            };
        }

        public static CourseEntity ToBllCourse(this DalCourse course)
        {
            return new CourseEntity()
            {
                Id = course.Id,
                Number = course.Number
            };
        }

        public static DalFaculty ToDalFaculty(this FacultyEntity faculty)
        {
            return new DalFaculty()
            {
                Id = faculty.Id,
                Name = faculty.Name
            };
        }

        public static FacultyEntity ToBllFaculty(this DalFaculty faculty)
        {
            return new FacultyEntity()
            {
                Id = faculty.Id,
                Name = faculty.Name
            };
        }

        public static DalGroup ToDalGroup(this GroupEntity group)
        {
            return new DalGroup()
            {
                Id = group.Id,
                Name = group.Name
            };
        }

        public static GroupEntity ToBllGroup(this DalGroup group)
        {
            return new GroupEntity()
            {
                Id = group.Id,
                Name = group.Name
            };
        }

        public static DalRole ToDalRole(this RoleEntity role)
        {
            return new DalRole()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static RoleEntity ToBllRole(this DalRole role)
        {
            return new RoleEntity()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static DalSpeciality ToDalSpeciality(this SpecialityEntity speciality)
        {
            return new DalSpeciality()
            {
                Id = speciality.Id,
                Name = speciality.Name
            };
        }

        public static SpecialityEntity ToBllSpeciality(this DalSpeciality speciality)
        {
            return new SpecialityEntity()
            {
                Id = speciality.Id,
                Name = speciality.Name
            };
        }

        public static DalTest ToDalTest(this TestEntity test)
        {
            return new DalTest()
            {
                Id = test.Id,
                Name = test.Name,
                TestCount = test.TestCount,
                TestTime = test.TestTime
            };
        }

        public static TestEntity ToBllTest(this DalTest test)
        {
            return new TestEntity()
            {
                Id = test.Id,
                Name = test.Name,
                TestCount = test.TestCount,
                TestTime = test.TestTime
            };
        }
    }
}
