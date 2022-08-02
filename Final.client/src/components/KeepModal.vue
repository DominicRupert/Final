<template>
  <Modal id="keep-modal">
    <template #modal-title>{{ keep.name }} by: {{keep.creator?.name}} </template>
    <template #modal-body>
      <div class="container-fluid">
        <div class="row">
          <img :src="keep.img" class="img-fluid" :alt="keep.img" />
          <h1>hello</h1>

            <!-- <h3>{{ keep.name }}</h3> -->
            <!-- <p>{{ keep.description }}</p> -->
            <!-- <img :src="keep.img" class="img-fluid py-2" alt="" /> -->
            <div class="dropdown">
  <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
    Add to Vault
  </button>
  <ul class="dropdown-menu">
    <li><a class="dropdown-item" href="#">Action</a></li>
    <li><a class="dropdown-item" href="#">Another action</a></li>
    <li><a class="dropdown-item" href="#">Something else here</a></li>
  </ul>
</div>
          </div>
        </div>
            <img
    @click="goToProfile"
    :src="keep.creator?.picture"
    class="pfp img-fluid p-0 rounded-pill selectable"
    alt=""
  />
    </template>
  </Modal>
</template>


<script>
import { computed, onMounted, reactive } from 'vue'
import { Modal } from 'bootstrap'
import { AppState } from '../AppState.js'
import { keepsService } from '../services/KeepsService.js'
import { useRouter } from 'vue-router'
import { logger } from '../utils/Logger.js'

export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },

  setup(props) {
    const router = useRouter()
    // const route = useRoute()
    return {
      async setActive() {
        try {
          await keepsService.setActive(props.keep)
        } catch (error) {
          logger.error(error)
        }
      },
      // activeKeep,
      // keep: reactive(route.params.keep),
      keep: computed(() => AppState.keeps),
      keep: computed(() => AppState.activeKeep),
      vault: computed(() => AppState.activeVault),
      profile: computed(() => AppState.profile),

      account: computed(() => AppState.account),

      vault: computed(() => AppState.vaults),
      

      async addToVault(vault) {
        try {
          editableKeep.vaultId = vault.id
          logger.log(editableKeep)
          await keepsService.addToVault(keep.id)
        } catch (e) {
          logger.error(e)
          Pop.toast(e.message)
        }
      },
         goToProfile() {
        Modal.getOrCreateInstance(document.getElementById("keep-modal")).hide()
        router.push({ name: 'Profile', params: { id: props.keep.creatorId } })
      },
    }
  }
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