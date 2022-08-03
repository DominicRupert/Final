<template>

  <button class="btn btn-dark " @click="goToVault">
    <h5>

      {{ vault.name }}

    </h5>
    <div>
    </div>
    <div>{{ vault.creator?.name }}</div>
    <!-- <img :src="vault.img" alt="" /> -->
  </button>

</template>


<script>
import { computed, onMounted } from 'vue'
import { logger } from '../utils/Logger.js'
import { AppState } from '../AppState.js'

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
    onMounted(async () => {
      // await vaultsService.getMyVaults();

    });
    const router = useRouter()
    const route = useRoute()
    return {
      async goToVault() {
        router.push({
          name: "Vault",
          params: {
            id: props.vault.id
          }
        })
        try {

          // if (props.vault.isPrivate && props.vault.creator.id !== AppState.account.Id) {
          //   Pop.toast('You do not have access to this vault')
          // }

        }
        catch (error) {
          logger.error(error);
        }
      },
      // vault: computed(() => AppState.vaults),
      // vault: computed(() => AppState.vaults.find(v => v.id === props.vault.id)),

      // account: computed(() => AppState.account),
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.profileVaults),
      // keeps: computed(() => AppState.profileKeeps),

      // avaults: computed(() => AppState.),

    }
  }
}
</script>


<style lang="scss" scoped>
</style>