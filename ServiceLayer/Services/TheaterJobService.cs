﻿using DataAccessLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class TheaterJobService
    {
        private readonly BroadwayBuilderContext _dbContext;

        public TheaterJobService(BroadwayBuilderContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void CreateTheaterJob(TheaterJobPosting theaterJob)
        {
            theaterJob.DateCreated = DateTime.Now;
            _dbContext.TheaterJobPostings.Add(theaterJob);
        }

        public TheaterJobPosting GetTheaterJob(TheaterJobPosting theaterJob)
        {
            return _dbContext.TheaterJobPostings.Find(theaterJob.HelpWantedID);
        }

        public TheaterJobPosting GetTheaterJob(int helpwantedid)
        {
            return _dbContext.TheaterJobPostings.Find(helpwantedid);
        }

        public IEnumerable GetAllJobsFromTheater(int theaterid)
        {
            return _dbContext.TheaterJobPostings.Where(job => job.TheaterID == theaterid)
                    .Select(job => new {
                        Title = job.Title, //Position = job.Position,
                        Hours = job.Hours,
                        Description = job.Description,
                        Requirement = job.Requirements
                    }).ToList();
        }
        public void UpdateTheaterJob(TheaterJobPosting updatedTheaterJob, TheaterJobPosting originalTheaterJob)
        {
            if (originalTheaterJob != null)
            {
                originalTheaterJob.Title = updatedTheaterJob.Title;
                originalTheaterJob.Hours = updatedTheaterJob.Hours;
                originalTheaterJob.Position = updatedTheaterJob.Position;
                originalTheaterJob.Requirements = updatedTheaterJob.Requirements;
                originalTheaterJob.theater = updatedTheaterJob.theater;
            }
        }

        public void UpdateTheaterJob(TheaterJobPosting updatedTheaterJob)
        {
            _dbContext.Entry(updatedTheaterJob).State = EntityState.Modified;
        }

        public void DeleteTheaterJob(TheaterJobPosting theaterJob)
        {
            TheaterJobPosting jobToRemove = _dbContext.TheaterJobPostings.Find(theaterJob.HelpWantedID);
            if (jobToRemove != null)
            {
                _dbContext.TheaterJobPostings.Remove(jobToRemove);
            }
        }
    }
}