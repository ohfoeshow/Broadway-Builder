<template>
  <div id="HelpWanted">

    <h1>
      <strong>Job Opportunities</strong> | {{ theater.TheaterName }}
    </h1>

    <div class="columns">
      <div class="column is-2 is-narrow">

        <div id="buttons" v-if="hasPermission === true">
          <a class="button is-medium" @click="addJob = true">Post A New Job</a>
        </div>
        <div id="buttons" v-else>
          <!-- Upload resume functionality -->
          <div class="file has-name is-boxed">
            <label class="file-label">
              <input class="file-input" type="file" ref="file" @change="onFileChange()">
              <span class="file-cta">
                <span class="file-label"><strong>Upload Your Resume</strong></span>
              </span>
              <span class="file-name" v-if="file === ''">No file uploaded...</span>
              <span class="file-name" v-else><a :href="file" target="_blank">Your Resume</a></span>
            </label>
          </div>
          <div id="buttons" v-if="hasPermission === false">
            <a class="button is-medium" @click="uploadResume()">Submit Resume</a>
          </div>
        </div>

        <!-- This is the JOB FILTER checkboxes (Still a work in progress) -->
        <JobFilter :jobPostings="jobs" @filtered="filterJobPostings" />
      </div>
      <div class="column is-10">

        <!-- This displays all jobs stored in the database as cards on the page -->
        <DisplayJobPostings v-if="hasPermission" :jobPostings="jobs" :hasPermission="true" :filters="filters" @deleteFinished="getTotalPages(), getJobPostings()" />
        <DisplayJobPostings v-else :jobPostings="jobs" :hasPermission="false" :filters="filters" :file="file" @deleteFinished="getTotalPages(), getJobPostings()" />
        <h1 v-if="jobs.length === 0">No job postings available</h1>
        <v-divider></v-divider>

        <!-- Pagination for job postings -->
        <div class="text-sm-center">
          <v-pagination v-model="currentPage" @input="choosePage(currentPage)" color="#6F0000" :length="totalPages" :total-visible="10"></v-pagination>
        </div>
      </div>
    </div>
    <AddJobModal v-if="addJob" :theaterId="theater.TheaterID" @cancel="addJob = false, getTotalPages(), getJobPostings()" />

  </div>
</template>

<script>
import AddJobModal from "@/components/HelpWanted/AddJobModal.vue";
import DisplayJobPostings from "@/components/HelpWanted/DisplayJobPostings.vue";
import JobFilter from "@/components/HelpWanted/JobFilter.vue";
import axios from "axios";

export default {
  name: "Help Wanted",
  props: ["theater", "hasPermission"],
  components: {
    AddJobModal,
    DisplayJobPostings,
    JobFilter
  },
  data() {
    return {
      // This array stores the jobs obtained from the database.
      jobs: [],
      // Stores the current page the user is on
      currentPage: 1,
      // The minimum number of pages
      minPage: 1,
      // The maximum number of pages (this will change)
      totalPages: 1,
      // The amount of jobs to retrive
      numberOfItems: 3,
      // Boolean value to display new job posting inputs
      addJob: false,
      // Filter applied to a job
      filters: [],
      // Resume file
      file: "",
      // Mocked user
      userId: 1
    };
  },
  methods: {
    filterJobPostings(jobFilters) {
      this.filters = jobFilters;
    },
    async choosePage(page) {
      this.currentPage = page;
      await this.getJobPostings();
    },
    prevPage() {
      this.currentPage -= 1;
      this.getJobPostings();
    },
    nextPage() {
      this.currentPage += 1;
      this.getJobPostings();
    },
    onFileChange() {
      this.file = this.$refs.file.files[0];
    },
    async uploadResume() {
      if (this.fileName === "") {
        alert("Please enter a resume...");
      } else {
        let formData = new FormData();
        formData.append(this.file.name, this.file);
        await axios
          .put(
            "https://api.broadwaybuilder.xyz/helpwanted/uploadresume",
            formData,
            {
              params: {
                userId: this.userId
              },
              headers: {
                "Content-Type": "multipart/form-data"
              }
            }
          )
          .then(response => alert(response.data))
          .catch(error => alert(error));
      }
    },
    async getResume() {
      await axios
        .get("https://api.broadwaybuilder.xyz/helpwanted/myresume", {
          params: {
            userId: this.userId
          }
        })
        .then(response => (this.file = response.data));
    },
    async getJobPostings() {
      // Obtain all jobs from the database within a range
      await axios
        .get("https://api.broadwaybuilder.xyz/helpwanted/gettheaterjobs", {
          params: {
            theaterId: this.theater.TheaterID,
            // The current page. This will be used to calculate starting point of query
            currentPage: this.currentPage,
            // The number of items starting at the startingPoint
            numberOfItems: this.numberOfItems
          }
        })
        // Set the jobs to the queryed job posting selection
        .then(
          response => (
            (this.jobs = response.data.JobPosting),
            (this.totalPages = response.data.Count)
          )
        );

      for (var i = 0; i < this.jobs.length; i++) {
        // Appends a "show" attribute to display more details about the job
        this.$set(this.jobs[i], "show", false);
        // Appends a "edit" attribute to check if a job is being editted
        this.$set(this.jobs[i], "edit", false);
      }
    }
  },
  created() {
    // On initial load, get initial jobs
    this.getJobPostings();
    // Get resume for the user (if it exists)
    this.getResume();
  }
};
</script>

<style lang="sass" scoped>
@import '../../../node_modules/bulma/bulma.sass'

#HelpWanted
  margin: 0 8em
  padding-bottom: 3.5em

#buttons
  text-align: center
  margin-top: 1em

h1 
  margin: 1em
  font-size: 30px

a
  margin-bottom: 0.5em

.columns
  margin: 1em

.card    
  margin: 1.25em 0 1.25em 0
  box-shadow: 0 14px 128px rgba(0,0,0,0.19), 0 10px 10px rgba(0,0,0,0.22)

.button
  box-shadow: 0px 8px 15px rgba(0, 0, 0, 0.1);
  transition: all 0.2s ease 0s;
  align: center

.button:hover
  background-color: #6F0000;
  box-shadow: 0px 15px 20px rgba(0, 0, 0, 0.4);
  color: #fff;
  transform: translateY(-7px);
  font-weight: bold

</style>
