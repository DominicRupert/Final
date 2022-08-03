<template>
        <KeepModal />

  <div class="masonry-container">
    <div class="row">
      <div class="col">

        <h1>Profile Page</h1>
        <h2>{{ profile.name }}</h2>
        <button v-if="activeProfile.id == account.id" @click="createVaults">
          new vault
        </button>
        <div v-for="v in vaults" :key="v.id">
          <Vault :vault="v" />
        </div>
        <img :src="profile.picture" class="img-fluid" alt="" />
        <!-- <p>{{ profile.description }}</p> -->
        <div v-for="k in keeps" :key="k.id" class="p-3">
          <Keep :keep="k" />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState.js'
import { computed, onMounted, watchEffect, ref } from 'vue'
import { profilesService } from '../services/ProfilesService.js'
import { keepsService } from '../services/KeepsService.js'
import { useRoute } from 'vue-router'
import { vaultsService } from '../services/VaultsService.js'
import { vaultKeepsService } from '../services/VaultKeepsService.js'
import { accountService } from '../services/AccountService.js'
import { logger } from '../utils/Logger.js'
import { Modal } from 'bootstrap'

import Pop from '../utils/Pop.js'
// import Keep from '../components/Keep.vue'



export default {
  name: 'Profile',
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const editable = ref({})
    watchEffect(()=>{
      AppState.profile
    })

    const route = useRoute();
    onMounted(async () => {
      try {

        // await keepsService.getKeeps()
        // await keepsService.setActive(props.keep)


        await profilesService.getProfile(route.params.id);
        await keepsService.getKeepsByUserId(route.params.id);
        await vaultsService.getVaultsByUserId(route.params.id);
        // await vaultKeepsService.getVaultKeeps(route.params.id);
        // await keepsService.getKeepsByUserId(route.params.id);
      }
      catch (error) {
        logger.error(error);
        Pop.toast(error.message);
      }

    })
    return {
      async createVaults() {
        try {
          const vault = await vaultsService.createVaults({
            name: 'New Vault',
            description: 'New Vault',
            userId: AppState.activeProfile
          });
          Pop.toast("Vault created");
        } catch (error) {
          logger.error(error);
          Pop.toast(error.message);
        }
      },
  route,
      activeProfile: computed(() => AppState.activeProfile),
      profile: computed(() => AppState.profile),
      account: computed(() => AppState.account),
     
      keeps: computed(() => AppState.profileKeeps),
      vaults: computed(() => AppState.profileVaults),
      user: computed(() => AppState.user),
      // vaultKeeps: computed(() => AppState.vaultKeeps),

    };
  },
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