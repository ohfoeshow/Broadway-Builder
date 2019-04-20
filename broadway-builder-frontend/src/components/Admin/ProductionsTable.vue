<template>
  <div class="ProductionsTable">
    <table class="table is-hoverable">
      <thead>
        <tr>Productions</tr>
        <tr>
          <th>Theater ID</th>
          <th>Production ID</th>
          <th>Production Name</th>
          <th>Director</th>
          <th>Address</th>
          <th>Created</th>
          <th>Edit</th>
          <th>Delete</th>
          <th>Upload New Program</th>
        </tr>
      </thead>
      <tbody v-for="(production, index) in productions" :key="index">
        <tr>
          <td>{{production.TheaterID}}</td>
          <td>{{production.ProductionID}}</td>
          <td>{{production.ProductionName}}</td>
          <td>{{production.DirectorFirstName}} {{production.DirectorLastName}}</td>
          <td>{{production.Street}}, {{production.City}}, {{production.StateProvince}} {{production.Zipcode}}</td>
          <td>{{production.DateTimes[0].Date}}</td>
          <td>
            <a v-on:click="showModal" v-bind:production="production">editing</a>
          </td>

          <td>
            <a v-on:click="deleteProduction(production.ProductionID)">deleting</a>
          </td>
          <td>
            <input
              type="file"
              id="file"
              ref="file"
              @change="programIDSelect(production.ProductionID), onFileChange"
            >
            <button
              v-if="programID === production.ProductionID"
              v-on:click="uploadProgram(production.ProductionID)"
              type="submit"
            >Submit</button>
          </td>
        </tr>
      </tbody>
    </table>
    <modal v-show="isModalVisible" @close="closeModal"/>
  </div>
</template>

<script>
import axios from "axios";
import Modal from "@/components/Modal.vue";
export default {
  name: "ProductionsTable",
  components: {
    Modal
  },
  data() {
    return {
      productions: [],
      isModalVisible: false,
      file: "",
      programID: Number
    };
  },
  async mounted() {
    await axios
      .get(
        "https://api.broadwaybuilder.xyz/production/getProductions?currentDate=3%2F23%2F2019"
      )
      .then(response => (this.productions = response.data));
  },
  methods: {
    async deleteProduction(ProductionID) {
      await axios
        .delete(
          "https://api.broadwaybuilder.xyz/production/delete/" + ProductionID
        )
        .then(alert("Production Successfully Deleted"));
    },
    async uploadProgram(ProductionID) {
      let formData = new FormData();
      formData.append("file", this.file);
      await axios
        .put(
          "https://api.broadwaybuilder.xyz/production/" +
            ProductionID +
            "/uploadProgram",
          formData
        )
        .then(alert("New Program Added!"));
    },
    showModal() {
      this.isModalVisible = true;
    },
    closeModal() {
      this.isModalVisible = false;
    },
    onFileChange(e) {
      var files = e.target.files || e.dataTransfer.files;
      if (!files.length) return;
      var reader = new FileReader();
      reader.onload = e => {
        this.program = e.target.result;
      };
      reader.readAsDataURL(files[0]);
    },
    programIDSelect(id) {
      this.programID = id;
    }
  }
};
</script>

<style lang="sass" scoped>
a
 color: black

</style>
