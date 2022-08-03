<template>
  <Modal id="keep-modal">
    <template #modal-title
      >{{ keep.name }} by: {{ keep.creator?.name }}
    </template>
    <template #modal-body>
      <div class="container-fluid">
      <img
        @click="goToProfile"
        :src="keep.creator?.picture"
        class="pfp img-fluid p-0 rounded-pill selectable"
        alt=""
      />
        <div class="row">
          <img :src="keep.img" class="img-fluid" :alt="keep.img" />
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
                <Vault @click="addToVault" :vault="v" />
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
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  props:{
    vault: {
      type: Object,
      required: true
    }
  },
  props:{
    vaultKeep: {
      type: Object,
      required: true
    }
  },
  
  setup(props) {
    const router = useRouter();
    // const route = useRoute()
    return {
      async setActive() {
        try {
          await keepsService.setActive(props.keep);
        }
        catch (error) {
          logger.error(error);
        }
      },
      async addToVaults(vault) {
        try {
          await keepsService.addToVault(props.vault);
        }
        catch (error) {
          logger.error(error);
        }
      },
      // activeKeep,
      // keep: reactive(route.params.keep),
      keep: computed(() => AppState.keeps),
      keep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.profileVaults),
      profile: computed(() => AppState.profile),
      account: computed(() => AppState.account),
      pkeeps: computed(() => AppState.profileKeeps),

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
.pfp {
  position: relative;
  top: -50px;
  left: 250px;
  width: 75px;
}
</style>