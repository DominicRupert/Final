<template>
  <div class="masonry-container">
    <!-- <div class="row"> -->
    <div class="col">
      <h1>Profile Page</h1>
      <Vault v-for="v in vaults" :vault="v" :key="v.id" />
      <!-- <h2>{{ profile.name }}</h2> -->
      <img :src="profile.picture" class="img-fluid" alt="" />
      <!-- <p>{{ profile.description }}</p> -->
      <Keep v-for="k in keeps" :keep="k" :key="k.id" />
    </div>
    <!-- </div> -->
  </div>
</template>


<script>
import { AppState } from '../AppState.js'
import { computed, onMounted, watchEffect } from 'vue'
import { profilesService } from '../services/ProfilesService.js'
import { keepsService } from '../services/KeepsService.js'
import { useRoute } from 'vue-router'
import { vaultsService } from '../services/VaultsService.js'
import { vaultKeepsService } from '../services/VaultKeepsService.js'
import { accountService } from '../services/AccountService.js'
import { logger } from '../utils/Logger.js'

import Pop from '../utils/Pop.js'
// import Keep from '../components/Keep.vue'



export default {
  name: 'Profile',
  setup() {
    watchEffect(() => {
        AppState.account;
        AppState.keeps;
    });
      const route = useRoute();
    onMounted(async () => {
      try {


        await profilesService.getProfile(route.params.id);
        await keepsService.getKeepsByUserId(route.params.id);
        // await vaultsService.getVaultsByUserId(route.params.id);
        // await vaultKeepsService.getVaultKeeps(route.params.id);
        // await keepsService.getKeepsByUserId(route.params.id);
      }
      catch (error) {
        logger.error(error);
        Pop.toast(error.message);
      }
    });
    return {
      route,
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.keeps),
      // vaults: computed(() => AppState.vaults),
      // keeps: computed(() => AppState.profileKeeps),
      // vaults: computed(() => AppState.profileVaults),
      // vaultKeeps: computed(() => AppState.vaultKeeps),

    };
  },
  // components: { Keep }
}
</script>


<style lang="scss" scoped>
.masonry-container {
  margin-top: 20px;
  margin-bottom: 20px;
  display: grid;
  padding: 2em;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  grid-auto-rows: minmax(100px, auto);
}
</style>