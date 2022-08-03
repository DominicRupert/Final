<template>

  <button class="btn btn-dark " @click="goToVault">
    <div>{{ vault.name }}</div>
    <!-- <div>{{ vault.creator?.name }}</div> -->
    <!-- <img :src="vault.img" alt="" /> -->
  </button>

</template>


<script>
import { computed, onMounted } from 'vue'
import { logger } from '../utils/Logger.js'
import { AppState } from '../AppState.js'
// import { vaultKeepsService } from '../services/VaultKeepsService.js'

import { vaultsService } from '../services/VaultsService.js'
import { useRouter } from 'vue-router'
import Pop from '../utils/Pop.js'
import { vaultKeepsService } from '../services/VaultKeepsService.js'
import { useRoute } from 'vue-router'
// import { useRouter } from 'vue-router'
import { keepsService } from '../services/KeepsService.js'
import { profilesService } from '../services/ProfilesService.js'

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
         router.push({
           name: "Vault",
           params: {
             id: props.vault?.id
           }
         })
        try {
          
          if (props.vault.isPrivate && props.vault.creator.id !== AppState.account.Id) {
            Pop.toast('You do not have access to this vault')
          }
          // await vaultsService.getVaultsByUserId(AppState.profileVaults.userId);
          // await vaultKeepsService.getVaultKeeps(props.vault.id);
          await vaultsService.getVaultById(route.params.id);
          // await vaultKeepsService.getVaultKeeps(props.vault.id);
          // await vaultsService.getVaultKeeps(props.vault.id);
          // vaultsService.getVaults()
        }
        catch (error) {
          logger.error(error);
        }
      },
      vault: computed(() => AppState.vaults),
      // vault: computed(() => AppState.vaults.find(v => v.id === props.vault.id)),

      // account: computed(() => AppState.account),
      profile: computed(() => AppState.profile),
      // pvaults: computed(() => AppState.profileVaults),
      // keeps: computed(() => AppState.profileKeeps),

      // avaults: computed(() => AppState.),

    }
  }
}
</script>


<style lang="scss" scoped>
</style>