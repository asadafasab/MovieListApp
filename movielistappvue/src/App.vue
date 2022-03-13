<template>
  <div>
    <div class="container">
      <div class="mt-6 is-flex is-flex-direction-column">
        <div>
          <h2 class="title mb-4 mt-4">{{ $t("title") }}</h2>
          <div class="">{{ $t("subtitle") }}</div>
          <button
            class="button mt-4 mb-6 is-fullwidth"
            @click="toggleMovieModal"
          >
            {{ $t("modals.addNewMovie") }}
          </button>
        </div>
        <table class="table has-text-left">
          <thead>
            <tr>
              <th>{{ $t("table.name") }}</th>
              <th>{{ $t("table.productionYear") }}</th>
              <th>{{ $t("table.actions") }}</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="movie of movies" :key="movie.id">
              <!-- <th>{{ movie.id }}</th> -->
              <td>{{ movie.name }}</td>
              <td>{{ movie.productionYear }}</td>
              <td>
                <nav class="breadcrumb" aria-label="breadcrumbs">
                  <ul>
                    <li>
                      <a href="#" @click="toggleDelete(movie.id)">
                        <span>{{ $t("modals.delete") }}</span>
                      </a>
                    </li>
                    <li>
                      <a
                        href="#"
                        @click="
                          toggleEditModal(
                            movie.id,
                            movie.name,
                            movie.productionYear
                          )
                        "
                      >
                        <span>{{ $t("modals.edit") }}</span>
                      </a>
                    </li>
                  </ul>
                </nav>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <!-- Add new movie modal -->
    <div class="modal is-active" v-if="state.isVisibleMovieModal">
      <div class="modal-background"></div>
      <div class="modal-card">
        <header class="modal-card-head">
          <p class="modal-card-title">{{ $t("modals.addNewMovie") }}</p>
          <button
            class="delete"
            aria-label="close"
            @click="toggleMovieModal"
          ></button>
        </header>
        <section class="modal-card-body">
          <input
            v-model="state.movieCurrent.name"
            class="input mb-2"
            type="text"
            :placeholder="$t('table.name')"
          />
          <span v-if="v$.movieCurrent.name.$error">
            {{ $t("modals.nameError") }}
          </span>
          <input
            v-model="state.movieCurrent.productionYear"
            class="input"
            type="number"
            :placeholder="$t('table.productionYear')"
          />
          <span v-if="v$.movieCurrent.productionYear.$error">
            {{ $t("modals.yearError") }}
          </span>
        </section>
        <footer class="modal-card-foot">
          <button class="button is-success" @click="addNewMovie">
            {{ $t("modals.save") }}
          </button>
          <button class="button" @click="toggleMovieModal">
            {{ $t("modals.cancel") }}
          </button>
        </footer>
      </div>
    </div>
    <!-- Edit movie modal -->
    <div class="modal is-active" v-if="state.isVisibleEditModal">
      <div class="modal-background"></div>
      <div class="modal-card">
        <header class="modal-card-head">
          <p class="modal-card-title">{{ $t("modals.editMovie") }}</p>
          <button
            class="delete"
            aria-label="close"
            @click="toggleEditModal"
          ></button>
        </header>
        <section class="modal-card-body">
          <input
            v-model="state.movieCurrent.name"
            class="input mb-2"
            type="text"
            :placeholder="$t('table.name')"
          />
          <span v-if="v$.movieCurrent.name.$error">
            {{ $t("modals.nameError") }}
          </span>
          <input
            v-model="state.movieCurrent.productionYear"
            class="input"
            type="number"
            :placeholder="$t('table.productionYear')"
          />
          <span v-if="v$.movieCurrent.productionYear.$error">
            {{ $t("modals.yearError") }}
          </span>
        </section>
        <footer class="modal-card-foot">
          <button class="button is-success" @click="editMovie">
            {{ $t("modals.save") }}
          </button>
          <button class="button" @click="toggleEditModal">
            {{ $t("modals.cancel") }}
          </button>
        </footer>
      </div>
    </div>

    <!-- Delete Modal -->
    <div class="modal is-active" v-if="state.isVisibleDelete">
      <div class="modal-background"></div>
      <div class="modal-card">
        <header class="modal-card-head">
          <p class="modal-card-title">{{ $t("modals.delete") }}</p>
        </header>
        <section class="modal-card-body">
          {{ $t("modals.confirmQuestion") }}
        </section>
        <footer class="modal-card-foot">
          <button class="button is-success" @click="deleteMovie">
            {{ $t("modals.yes") }}
          </button>
          <button class="button" @click="toggleDelete">
            {{ $t("modals.cancel") }}
          </button>
        </footer>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import useValidate from "@vuelidate/core";
import {
  required,
  minLength,
  numeric,
  maxValue,
  minValue,
} from "@vuelidate/validators";
import { reactive, computed } from "@vue/composition-api";

const baseURL = "/api/";

export default {
  name: "App",
  components: {},
  setup() {
    const state = reactive({
      movieCurrent: {
        id: null,
        name: "",
        productionYear: null,
      },
      //modals
      isVisibleDelete: false,
      isVisibleMovieModal: false,
      isVisibleEditModal: false,
    });
    const rules = computed(() => {
      return {
        movieCurrent: {
          name: { required, minLength: minLength(1) },
          productionYear: {
            numeric,
            minValue: minValue(1900),
            maxValue: maxValue(2100),
          },
        },
      };
    });
    const v$ = useValidate(rules, state);
    return {
      state,
      v$,
    };
  },
  data() {
    return {
      movies: [],
      deleteId: null,
    };
  },
  async created() {
    await axios
      .get(baseURL + "Movie")
      .then((res) => {
        this.movies = res.data;
      })
      .catch((error) => {
        console.log(error.message);
      });
  },
  methods: {
    toggleMovieModal() {
      this.state.isVisibleMovieModal = !this.state.isVisibleMovieModal;
    },
    toggleEditModal(id, name, year) {
      this.state.isVisibleEditModal = !this.state.isVisibleEditModal;
      if (this.state.isVisibleEditModal && id != null) {
        this.state.movieCurrent.id = id;
        this.state.movieCurrent.name = name;
        this.state.movieCurrent.productionYear = year;
      } else {
        this.state.movieCurrent.id = null;
        this.state.movieCurrent.name = "";
        this.state.movieCurrent.productionYear = null;
      }
    },
    toggleDelete(id) {
      if (this.state.isVisibleDelete) {
        this.deleteId = null;
      } else {
        this.deleteId = id;
      }
      this.state.isVisibleDelete = !this.state.isVisibleDelete;
    },
    async deleteMovie() {
      await axios
        .delete(baseURL + `Movie?id=${this.deleteId}`)
        .then((res) => {
          this.movies = res.data;
        })
        .catch((error) => {
          alert(error);
        });
      this.state.isVisibleDelete = false;
    },
    async addNewMovie() {
      //validation
      this.v$.$validate();
      if (this.v$.$error) {
        return;
      }
      this.state.isVisibleMovieModal = false;
      if (this.state.movieCurrent.productionYear == "") {
        this.state.movieCurrent.productionYear = null;
      }

      await axios
        .post(baseURL + "Movie", {
          name: this.state.movieCurrent.name,
          ProductionYear: this.state.movieCurrent.productionYear,
        })
        .then((res) => {
          this.movies = res.data;
          this.state.movieCurrent.name = "";
          this.state.movieCurrent.productionYear = null;
        })
        .catch((error) => {
          alert(error);
        });
    },
    async editMovie() {
      //validation
      this.v$.$validate();
      if (this.v$.$error) {
        return;
      }
      this.state.isVisibleEditModal = false;

      await axios
        .put(baseURL + "Movie", {
          id: this.state.movieCurrent.id,
          name: this.state.movieCurrent.name,
          ProductionYear: this.state.movieCurrent.productionYear,
        })
        .then((res) => {
          this.movies = res.data;
          this.state.movieCurrent.id = null;
          this.state.movieCurrent.name = "";
          this.state.movieCurrent.productionYear = null;
        })
        .catch((error) => {
          alert(error);
        });
    },
  },
};
</script>