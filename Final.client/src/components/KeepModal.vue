<template>
  <Modal id="keep-modal" class="keepmodal">
    <template #modal-title
      >{{ keep.name }} by: {{ keep.creator?.name }}
      <img
        @click="goToProfile"
        :src="keep.creator?.picture"
        class="pfp selectable object-fit rounded-circle"
        alt=""
      />
    </template>
    <template #modal-body>
      <div class="container-fluid position-relative">
        <h3>View Count: {{keep.views}}</h3>
        <h3>Kept Count: {{keep.kept}}</h3>
        <div class="row">
        <div class="col-md-6">
          <img :src="keep.img" class="w-100 object-fit keepimg" :alt="keep.img" />
        </div>
          <h1>hello</h1>

          <!-- <h3>{{ keep.name }}</h3> -->
          <!-- <p>{{ keep.description }}</p> -->
          <!-- <img :src="keep.img" class="img-fluid py-2" alt="" /> -->
          <div></div>
          <div class="dropdown">
            <button
              class="btn btn-secondary dropdown-toggle"
              type="button"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            >
              Add to Vault
            </button>
            <ul class="dropdown-menu">
              <li v-for="v in vaults" :key="v.id">
                <a @click="addToVault(v.id)">{{ v.name }}</a>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed, onMounted, reactive, ref } from 'vue'
import { Modal } from 'bootstrap'
import { AppState } from '../AppState.js'
import { keepsService } from '../services/KeepsService.js'
import { vaultKeepsService } from '../services/VaultKeepsService.js'
import { useRouter } from 'vue-router'
import { useRoute } from 'vue-router'

import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'
export default {
 
  setup(props) {
    const router = useRouter();
    const route = useRoute()
    return {
      
      // async setActive() {
      //   try {
      //     AppState.keeps.views++;
      //     await keepsService.getKeep(props.keep);
      //   }
      //   catch (error) {
      //     logger.error(error);
      //   }
      // },
      // activeKeep,
      // keep: reactive(route.params.keep),
      // keep: computed(() => AppState.keeps),
      // vaults: computed(() => AppState.vaults),
      keep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.myVaults),
      profile: computed(() => AppState.profile),
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.profileKeeps),
      vaultKeep: computed(() => AppState.vaultKeeps),
      async addToVault(id) {
        try {
          await keepsService.addToVault(AppState.activeKeep.id, id );
        }
        catch (error) {
          logger.error(error);
        }
      },

      // vault: computed(() => AppState.vaults),
      goToProfile() {
        Modal.getOrCreateInstance(document.getElementById("keep-modal")).hide();
        router.push({ name: "Profile", params: { id: AppState.keeps?.creatorId } });
      },
    //  async createVaultKeeps(){
    //   try {
    //     await vaultKeepsService.createVaultKeeps(props.vaultKeep);
    //   }
    //   catch (error) {
    //     logger.error(error);
    //   }
        
    //  }
    };
  },
}
</script>


<style lang="scss" scoped>
.keepmodal{
 
}
.pfp{
   height: 200px;
  width: 200px;
}
.keepimg{min-height: 70vh;}
</style>