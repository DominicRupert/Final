<template>
  <div class="profile-container">
    <div class="row">
      <div class="col-md-12">
        <h1>Profile Page</h1>
        <img :src="profile.picture" alt="" />
        <h2>{{ profile.name }}</h2>
      </div>
    </div>
  </div>

  <div class="masonry-container">
    <div v-for="k in keeps" :key="k.id">
      <Keep :keep="k" />
    </div>
  </div>
  <div>
    <!-- <button @click="newVault">NEW VAULT</button> -->
  </div>
</template>


<script>
import { computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState.js'
import { useRouter } from 'vue-router'
import { profilesService } from '../services/ProfilesService.js'
import { logger } from '../utils/Logger.js'
import { keepsService } from '../services/KeepsService.js'

import Pop from '../utils/Pop.js'
import { vaultsService } from '../services/VaultsService.js'
import { Modal } from 'bootstrap'
// import Keep from '../components/Keep.vue'



export default {
  // props: {
  //   vault: {
  //     type: Object,
  //     required: true
  //   }
  // },
  name: 'Profile',
  setup() {
    const route = useRoute();
    onMounted(async () => {
      await keepsService.getKeepsByUserId(route.params.id)
      await vaultsService.getVaultsByUserId(route.params.id)

      await profilesService.getProfile(route.params.id);
      // await keepsService.getKeepsByUserId(route.params.id);


    });
    return {
      // route,
      profile: computed(() => AppState.profile),
      activeKeep: computed(() => AppState.activeKeep),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),


      pkeeps: computed(() => AppState.profileKeeps),
      pvaults: computed(() => AppState.profileVaults),

      // keeps: computed(() => AppState.keeps),
      // account: computed(() => AppState.account),
      // async newVault(){
      //   try{
      //     AppState.vaults = props.vaults
      //     await vaultsService.getVaults()
      //     Modal.getOrCreateInstance(document.getElementById("vault-modal")).show()

      //   }catch(e){
      //     logger.error(e)
      //   }
      // }

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