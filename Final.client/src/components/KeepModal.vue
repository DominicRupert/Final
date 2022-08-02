<template>
  <Modal id="keep-modal">
    <template #modal-title>{{ keep.name }}</template>
    <template #modal-body>
      <div class="container-fluid">
        <div class="row">
          <div class="col-md-6">
            
            <!-- <h3>{{ keep.name }}</h3> -->
          <!-- <p>{{ keep.description }}</p> -->
          <img :src="keep.img" class="img-fluid" :alt="keep.img" />
          <!-- <p @click="goToProfile">{{ keep.name }}</p> -->
          <!-- <img :src="keep.img" class="img-fluid py-2" alt="" /> -->
          <!-- <button class="btn btn-dark"><h3>Add To Vault</h3></button> -->
        </div>
          </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { AppState } from '../AppState.js'
import { keepsService } from '../services/KeepsService.js'
import { computed, onMounted, reactive } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { logger } from '../utils/Logger.js'

export default {

  setup() { 
    const router = useRouter()
    const route = useRoute()
    return {
      keep: computed(() => AppState.keeps),
        activeKeep: computed(() => AppState.activeKeep),
        activeVault: computed(() => AppState.activeVault),

      account: computed(() => AppState.account),

      vaults: computed(() => AppState.vaults),
      user: computed(() => AppState.user),

      async addToVault(vault) {
        try {
            editableKeep.vaultId = vault.id
            logger.log(editableKeep)            
          await keepsService.addToVault(this.keep)
        } catch (e) {
          logger.error(e)
          Pop.toast(e.message)
        }
      },
      goToProfile() {
        router.push({ name: 'Profile', params: { id: props.keep.creatorId } })
      },
    }
  }
}
</script>


<style lang="scss" scoped>
</style>