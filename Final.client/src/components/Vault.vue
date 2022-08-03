<template>
  <button @click="goToVault">
    <div>{{ vault.name }}</div>
    <div>{{ vault.creator?.name }}</div>
    <img :src="vault.img" alt="" />
  </button>
</template>


<script>
import { computed, onMounted } from 'vue'
import { logger } from '../utils/Logger.js'
import { AppState } from '../AppState.js'

import { vaultsService } from '../services/VaultsService.js'
import { useRouter } from 'vue-router'
import Pop from '../utils/Pop.js'

export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter()
    return {
      async goToVault() {
        try {
          if (props.vault.isPrivate && props.vault.creator.id !== AppState.user.id) {
            Pop.toast('You do not have access to this vault')
            return
          }
          await vaultsService.getVaultById(props.vault.id)
          await vaultsService.getVaultKeeps(props.vault.id)
          router.push({
            name: "Vault",
            params: {
              id: props.vault.id
            }
          })
        }
        catch (error) {
          logger.error(error);
        }
      },
      vaults: computed(() => AppState.vaults),

      account: computed(() => AppState.account),
      profile: computed(() => AppState.profile),
      pvaults: computed(() => AppState.profileVaults),
      keeps: computed(() => AppState.profileKeeps),

      // avaults: computed(() => AppState.),

    }
  }
}
</script>


<style lang="scss" scoped>
</style>