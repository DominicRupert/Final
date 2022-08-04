<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-secondary px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
      <button class="mdi mdi-home btn btn-outline-dark"></button>
      </div>
    </router-link>
    <router-link class="navbar-brand d-flex" :to="{ name: 'Profile', params: {id: account.id} }">
      <div class="d-flex flex-column align-items-center">
      <button class="mdi mdi-head btn btn-outline-dark"></button>
      </div>
    </router-link>
  
    <button
      class="navbar-toggler"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon" />
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
     
        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
import { AppState } from '../AppState.js';
import { computed, onMounted, reactive, ref } from 'vue'
export default {
  setup() {
    return {
      profile: computed(() => AppState.profile),
      account: computed(() => AppState.account),
    };
  },
};
</script>

<style scoped>
a:hover {
  text-decoration: none;
}
.nav-link {
  text-transform: uppercase;
}
.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
</style>
