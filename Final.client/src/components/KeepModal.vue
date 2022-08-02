<template>
  <Modal id="keep-modal">
    <template #modal-title>{{ keep.name }} by: {{ keep.creator?.name }} </template>
    <template #modal-body>
      <div class="container-fluid">
        <div class="row">
          <img :src="keep.img" class="img-fluid" :alt="keep.img" />
          <h1>hello</h1>

          <!-- <h3>{{ keep.name }}</h3> -->
          <!-- <p>{{ keep.description }}</p> -->
          <!-- <img :src="keep.img" class="img-fluid py-2" alt="" /> -->
            <div >
              
            </div>
          <div class="dropdown">
            <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown"
              aria-expanded="false">
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
      <img @click="goToProfile" :src="keep.creator?.picture" class="pfp img-fluid p-0 rounded-pill selectable" alt="" />
    </template>
  </Modal>
</template>


<script>
import { computed, onMounted, reactive } from 'vue'
import { Modal } from 'bootstrap'
import { AppState } from '../AppState.js'
import { keepsService } from '../services/KeepsService.js'
import { vaultKeepsService } from '../services/VaultKeepsService.js'
import { useRouter } from 'vue-router'
import { logger } from '../utils/Logger.js'
import  Pop  from '../utils/Pop.js'
export default {
  props: {
    keep: {
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
          await vaultKeepsService.addToVault(vault);
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
      // vault: computed(() => AppState.vaults),
      async addToVault(vault) {
        try {
          // await vaultKeepsService.addToVault(vault, props.keep);
          await keepsService.addToVault(vault, props.keep);
        }
        catch (e) {
          logger.error(e);
          Pop.toast(e.message);
        }
      },
      goToProfile() {
        Modal.getOrCreateInstance(document.getElementById("keep-modal")).hide();
        router.push({ name: "Profile", params: { id: props.keep.creatorId } });
      },
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