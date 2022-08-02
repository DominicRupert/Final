<template>
  <div class="about text-center">
    <h1>Welcome {{ account.name }}</h1>
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
    <div class="masonry-frame">
    <div v-for=" k in keeps " :key="k.id">
    <Keep :keep="k" />
    </div>
    </div>

  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { keepsService } from '../services/KeepsService.js'
import { useRoute } from 'vue-router'

import { AppState } from '../AppState'
import { logger } from '../utils/Logger.js'
import { profilesService } from '../services/ProfilesService.js'
import { accountService } from '../services/AccountService.js'
import { vaultsService } from '../services/VaultsService.js'
import  Pop  from '../utils/Pop.js'
import { Modal } from 'bootstrap'
export default {
  name: 'Account',
  setup() {
    const route = useRoute();
    onMounted(async () => {
      try {
       await  accountService.getAccountKeeps(route.params.id);
       await accountService.getAccountVaults(route.params.id);
       
      }
      catch (error) {
        logger.error(error);
        Pop.toast(error.message);
      }
    });
    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),

      vaults: computed(() => AppState.vaults),
      akeeps: computed(() => AppState.accountKeeps),
      avaults: computed(() => AppState.accountVaults),

    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
