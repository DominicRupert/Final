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
        
        <div class="row">
        <div class="col-md-6 d-flex">
          <img :src="keep.img" class="w-100 h-100 object-fit keepimg" :alt="keep.img" />
        </div>
        <div class="col-md-6 align-items-center flex-column justify-content-center d-flex">
        <h3>View Count: {{keep.views}}</h3>
        <h3>Kept Count: {{keep.kept}}</h3> 
        <h3>Description: {{keep.description}}</h3>
        
          <div>
             <button
        v-if="keep.creator.id == account.id"
        class="mdi mdi-delete btn text-danger btn-success"
        @click.stop="deleteVaultKeeps(vault.id)"
      >Remove from vault?</button>
          </div>
        </div>

          <!-- <h3>{{ keep.name }}</h3> -->
          <!-- <p>{{ keep.description }}</p> -->
          <!-- <img :src="keep.img" class="img-fluid py-2" alt="" /> -->
        </div>
      </div>
      <div class="justify-content-center d-flex col-6">
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
                <a @click="addToVault(v.id) " class="dropdown-item">{{ v.name }}</a>
              </li>
            </ul>
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
  props: {
    keep: {
      type: Object,
    },
    vaultkeep: {
      type: Object,
    }
  },

 
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
      vault: computed(() => AppState.activeVault),
      async addToVault(id) {
        try {
          await keepsService.addToVault(AppState.activeKeep.id, id );
          Pop.toast("Keep added to vault", "success");
        }
        catch (error) {
          logger.error(error);
        }
      },

      // vault: computed(() => AppState.vaults),
      goToProfile() {
        Modal.getOrCreateInstance(document.getElementById("keep-modal")).hide();
        router.push({ name: "Profile", params: { id: AppState.activeKeep?.creatorId } });
      },
      async deleteVaultKeeps() {
        try {
          if (await Pop.confirm("Are you sure you want to remove this keep?")) {
            await vaultKeepsService.removeKeep(props.vaultkeep.id)
            Pop.toast("Keep removed")

          }

        }
        catch (error) {
          logger.error(error)
        }

      }
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